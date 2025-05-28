<template>
  <div>
    <el-form :model="patient" label-width="80px" style="max-width: 500px;">
      <el-form-item label="姓名">
        <el-input v-model="patient.name" v-if="isEditing"></el-input>
        <span v-else>{{ patient.name }}</span>
      </el-form-item>
      <el-form-item label="性别">
        <el-radio-group v-model="patient.gender" v-if="isEditing">
          <el-radio :value="1">男</el-radio>
          <el-radio :value="2">女</el-radio>
        </el-radio-group>
        <span v-else>{{ getGenderValue(patient.gender) }}</span>
      </el-form-item>
      <el-form-item label="电话">
        <el-input v-model="patient.phone" v-if="isEditing"></el-input>
        <span v-else>{{ patient.phone }}</span>
      </el-form-item>
      <el-form-item label="用户名" v-if="!isEditing">
        <el-input v-model="patient.username" v-if="isEditing"></el-input>
        <span v-else>{{ patient.username }}</span>
      </el-form-item>
      <!-- 添加两个按钮 -->
      <el-button v-if="!isEditing" type="primary" @click="isEditing = true">修改信息</el-button>
      <el-button v-else type="primary" @click="toggleEdit">保存</el-button>
      <el-button type="danger" @click="showPasswordDialog = true">修改密码</el-button>
    </el-form>

    <!-- 修改密码的对话框 -->
    <el-dialog title="修改密码" v-model="showPasswordDialog" width="500">
      <el-form :model="passwordForm" label-width="80px">
        <el-form-item label="旧密码">
          <el-input type="password" v-model="passwordForm.oldPassword"></el-input>
        </el-form-item>
        <el-form-item label="新密码">
          <el-input type="password" v-model="passwordForm.newPassword"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="updatePassword">确认修改</el-button>
          <el-button @click="showPasswordDialog = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup>
import { GetPatientByUid, UpdatePassword, UpdatePatient } from '@/http/Patient';
import router from '@/router/router';
import { useRoleStore } from '@/stores/role';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';

const patient = ref({
  patient_id: "",
  user_id: "",
  name: "",
  gender: -1,
  phone: "",
  username: "",
  password: "",
})

const gender = [{
  value: 1,
  label: "男"
}, {
  value: 2,
  label: "女"
}]

const role = useRoleStore();
//是否显示编辑表单
const isEditing = ref(false);
//是否显示更改密码对话框
const showPasswordDialog = ref(false);
//新密码
const passwordForm = ref({
  oldPassword: '',
  newPassword: ''
});

//获取患者信息
const fetchPatient = async () => {
  try {
    const res = (await GetPatientByUid(role.user_id)).data;
    if (res.isSuccess) {
      patient.value = res.result;
      patient.value.username = role.username;
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('获取患者数据失败');
  }
};

//获取性别
const getGenderValue = (id: number) => {
  const value = gender.find((item) => item.value === id);
  return value ? value.label : "未知性别";
}

//修改个人信息
const toggleEdit = async () => {
  try {
    const data = {
      user_id: role.user_id,
      name: patient.value.name,
      gender: patient.value.gender,
      phone: patient.value.phone,
    }
    const res = (await UpdatePatient(data)).data;
    if (res.isSuccess) {
      isEditing.value = false;
      fetchPatient()
      ElMessage.success("修改成功")
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("修改失败")
  }
};

//更改密码
const updatePassword = async () => {
  try {
    const data = {
      username: role.username,
      password: passwordForm.value.oldPassword
    }
    const pass = passwordForm.value.newPassword
    const res = (await UpdatePassword(data, pass)).data;
    if (res.isSuccess) {
      showPasswordDialog.value = false;
      ElMessage.success("修改成功")
      // 清空所有 store 状态
      role.$reset();
      // 导航到登录页
      router.push('/');
    }
  } catch (error) {
    ElMessage.error("修改失败")
  }
};

onMounted(async () => {
  // 初始化获取患者数据
  await fetchPatient();
})
</script>
