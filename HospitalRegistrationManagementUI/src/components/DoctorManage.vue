<template>
  <el-table :data="tableData" style="width: 100%" max-height="700">
    <el-table-column fixed prop="doctor_id" label="医生id" width="100" />
    <el-table-column fixed prop="user_id" label="账号id" width="100" />
    <el-table-column prop="department_id" label="科室id" width="200">
      <template #default="{ row }">
        <el-select v-if="row.isEditing" v-model="row.department_id" placeholder="选择科室" size="large"
          style="width: 150px">
          <el-option v-for="item in department" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
        <span v-else>
          {{ getDepartmentName(row.department_id) }}
        </span>
      </template>
    </el-table-column>
    <el-table-column prop="name" label="医生姓名" width="150">
      <template #default="{ row }">
        <el-input v-if="row.isEditing" v-model="row.name" style="width: 80px" placeholder="请输入医生姓名" />
        <span v-else>{{ row.name }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="title" label="职称" width="150">
      <template #default="{ row }">
        <el-input v-if="row.isEditing" v-model="row.title" style="width: 80px" placeholder="请输入请输入职称" />
        <span v-else>{{ row.title }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="bio" label="医生简介" width="300">
      <template #default="{ row }">
        <el-input v-if="row.isEditing" v-model="row.bio" style="width: 200px" autosize type="textarea"
          placeholder="请输入医生简介" />
        <el-input v-else :readonly="read" v-model="row.bio" style="width: 200px" autosize type="textarea"
          placeholder="请输入医生简介" />
      </template>
    </el-table-column>
    <el-table-column prop="avatar_url" label="头像" width="200">
      <template #default="{ row }">
        <el-upload v-if="row.isEditing" class="upload" action="#" :before-upload="handleBeforeUpload"
          :on-success="handleSuccess" :file-list="fileList">
          <el-button slot="trigger" size="small" type="primary">选择文件</el-button>
          <template #tip>
            <div class="el-upload__tip">
              上传文件大小不能超过 2MB
            </div>
          </template>
        </el-upload>
        <span v-else><el-avatar :size="50" :src="row.avatar_url" /></span>
      </template>
    </el-table-column>
    <el-table-column fixed="right" label="选项" min-width="120">
      <template #default="{ row }">
        <el-button v-if="!row.isEditing" link type="primary" size="small" @click="editRow(row)">
          修改
        </el-button>
        <el-button v-if="row.isEditing" link type="success" size="small" @click="saveRow(row)">
          保存
        </el-button>
        <el-button link type="danger" size="small" @click="deleteRow(tableData.indexOf(row))">
          删除
        </el-button>
      </template>
    </el-table-column>
  </el-table>
  <el-button class="mt-4" style="width: 100%" @click="dialogVisible = true">
    新增
  </el-button>


  <el-dialog v-model="dialogVisible" title="新增医生" width="500" :close-on-click-modal="true" center>
    <el-form :model="CDoctor" label-width="auto">
      <el-form-item label="科室名:">
        <el-select v-model="CDoctor.department_id" placeholder="选择科室" size="large" style="width: 120px">
          <el-option v-for="item in department" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
      </el-form-item>
      <el-form-item label="医生姓名:">
        <el-input v-model="CDoctor.name" style="width: 300px" placeholder="请输入姓名"></el-input>
      </el-form-item>
      <el-form-item label="职称:">
        <el-input v-model="CDoctor.title" style="width: 300px;" placeholder="请输入职称"></el-input>
      </el-form-item>
      <el-form-item label="医生简介:">
        <el-input v-model="CDoctor.bio" style="width: 300px" autosize type="textarea" placeholder="请输入医生简介"></el-input>
      </el-form-item>
      <el-form-item label="登陆账号:">
        <el-input v-model="CDoctor.username" style="width: 300px;" placeholder="请输入注册账号"></el-input>
      </el-form-item>
      <el-form-item label="登陆密码:">
        <el-input v-model="CDoctor.password" style="width: 300px;" placeholder="请输入密码" type="password"></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <div class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="CreateDocForm">
          确定
        </el-button>
      </div>
    </template>
  </el-dialog>
</template>


<script lang="ts" setup>
import { UpAvartar } from '@/http/Avatar';
import { GetDepartmentAll } from '@/http/Department';
import { CreateDoctor, DeleteDoctor, GetDoctorAll, UpdateDoctor } from '@/http/Doctor';
import { ElMessage, ElMessageBox } from 'element-plus';
import { onMounted, ref } from 'vue';


interface TableRow {
  doctor_id: number;//医生id
  user_id: number;//用户id
  department_id: number;//科室id
  name: string;//医生名称
  title: string;//职称
  bio: string;//简介
  avatar_url: string;//头像URL
  isEditing: boolean; // 是否处于编辑状态
}

interface Department {
  value: number; // 科室id
  label: string; // 科室名称
}

const CDoctor = ref({
  department_id: '',//科室id
  name: '',//医生姓名
  title: '',//职称
  bio: '',//简介
  username: '',//登录账号
  password: '',//密码
})

const read = true;

const dialogVisible = ref(false)
// 表格数据
const tableData = ref<TableRow[]>([]);
// 科室数据
const department = ref<Department[]>([]);
//头像url
let avatar_url = ref('');

//获取医生数据
const fetchDoctors = async () => {
  try {
    const response = (await GetDoctorAll()).data;
    if (response.isSuccess) {
      // 将后端返回的科室数据转换为表格需要的格式
      tableData.value = response.result.map((item: any) => ({
        doctor_id: item.doctor_id,//医生id
        user_id: item.user_id,//用户id
        department_id: item.department_id,//科室id
        name: item.name,//医生名称
        title: item.title,//职称
        bio: item.bio,//医生简介
        avatar_url: item.avatar_url,//头像URL
        isEditing: false, // 初始状态为非编辑模式
      }),
      avatar_url=response.result.avatar_url
    );
    } else {
      ElMessage.error(response.msg);
    }
  } catch (error) {
    ElMessage.error('获取医生数据失败');
  }
}


// 获取科室数据
const fetchDepartments = async () => {
  try {
    const response = (await GetDepartmentAll()).data;
    // console.log(response)
    if (response.isSuccess) {
      department.value = response.result.map((dept: any) => ({
        value: dept.department_id,
        label: dept.name,
      }));
    } else {
      ElMessage.error(response.msg);
    }
  } catch (error) {
    ElMessage.error('获取科室数据失败');
  }
};

// 根据科室 ID 获取科室名称
const getDepartmentName = (department_id: number) => {
  const dept = department.value.find((item) => item.value === department_id);
  return dept ? dept.label : '未知科室';
};


//进入编辑模式
const editRow = async (row: TableRow) => {
  row.isEditing = true;
}

//保存修改
const saveRow = async (row: TableRow) => {
  try {
    const data = {
      doctor_id: row.doctor_id,
      user_id: row.user_id,
      department_id: row.department_id,
      name: row.name,
      title: row.title,
      bio: row.bio,
      avatar_url: avatar_url,
    }
    console.log(data)
    //调用后端Api更新数据
    const res = (await UpdateDoctor(data)).data;
    if (res.isSuccess) {
      ElMessage.success('保存成功');
      fetchDoctors()
      row.isEditing = false;
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('保存失败');
  }
}

//创建
const CreateDocForm = async () => {
  try {
    const data = {
      department_id: CDoctor.value.department_id,
      name: CDoctor.value.name,
      title: CDoctor.value.title,
      bio: CDoctor.value.bio,
      username: CDoctor.value.username,
      password: CDoctor.value.password
    };
    const res = (await CreateDoctor(data)).data;
    if (res.isSuccess) {
      ElMessage.success('保存成功');
      // 重置CDoctor对象为初始状态
      CDoctor.value = {
        department_id: '',
        name: '',
        title: '',
        bio: '',
        username: '',
        password: ''
      };
      dialogVisible.value = false;
      // 更新数据
      await fetchDoctors();
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('保存失败');
  }
}

//删除行
const deleteRow = async (index: number) => {
  try {
    const row = tableData.value[index];
    const res = (await DeleteDoctor(row.doctor_id)).data
    if (res.isSuccess) {
      tableData.value.splice(index, 1);
      ElMessage.success('删除成功');
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('删除失败');
  }
}

//上传文件列表数据
const fileList = ref([]);

//上传检查
const handleBeforeUpload = (file: any) => {
  const isImage = file.type.match(/^image\/(jpeg|png|gif|bmp|webp|tiff)$/);
  if (!isImage) {
    ElMessage.error('只能上传 JPEG, PNG, GIF, BMP, WEBP, TIFF 格式的图片!');
    return false;
  }
  // 将文件大小限制放宽到2MB
  const isLt2MB = file.size / 1024 / 1024 < 2;
  if (!isLt2MB) {
    ElMessage.error('上传文件大小不能超过 2MB!');
    return false;
  }
  uploadFile(file);
  return false;
};

const handleSuccess = (res: any, file: any) => {
  ElMessage.success('文件上传成功!');
  // 处理响应数据，例如更新文件列表或显示图片
};

//文件上传
const uploadFile = async (file: any) => {
  try {
    const res = (await UpAvartar(file)).data;
    if (res.isSuccess) {
      avatar_url = res.result;
      handleSuccess(res, file);
    }
  } catch (error) {
    ElMessage.error('文件上传失败!');
  }
};

onMounted(async () => {
  await fetchDepartments();
  await fetchDoctors();
})


</script>
