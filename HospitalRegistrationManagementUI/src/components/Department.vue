<template>
  <el-table :data="tableData" style="width: 100%" max-height="700">
    <el-table-column fixed prop="department_id" label="科室id" width="150" />
    <el-table-column prop="name" label="科室名称" width="180">
      <template #default="{ row }">
        <el-input v-if="row.isEditing || row.isNew" v-model="row.name" style="width: 150px" placeholder="请输入科室名称" />
        <span v-else>{{ row.name }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="description" label="科室描述" width="400">
      <template #default="{ row }">
        <el-input v-if="row.isEditing || row.isNew" v-model="row.description" style="width: 240px" autosize
          type="textarea" placeholder="请输入科室描述" />
        <el-input v-else :readonly="read" v-model="row.description" style="width: 240px" autosize type="textarea"
          placeholder="请输入科室描述" />
      </template>
    </el-table-column>
    <el-table-column fixed="right" label="选项" min-width="120">
      <template #default="{ row }">
        <el-button v-if="!row.isEditing && !row.isNew" link type="primary" size="small" @click="editRow(row)">
          修改
        </el-button>
        <el-button v-if="row.isEditing" link type="success" size="small" @click="saveRow(row)">
          保存
        </el-button>
        <el-button v-if="row.isNew" link type="success" size="small" @click="createRow(row)">
          保存
        </el-button>
        <el-button link type="danger" size="small" @click="deleteRow(tableData.indexOf(row))">
          删除
        </el-button>
      </template>
    </el-table-column>
  </el-table>
  <el-button class="mt-4" style="width: 100%" @click="onAddItem">
    添加
  </el-button>
</template>

<script lang="ts" setup>
import { CreateDepartment, DeleteDepartment, GetDepartmentAll, UpdateDepartment } from '@/http/Department';
import { ElMessage, type Table } from 'element-plus';
import { onMounted, ref } from 'vue';


interface TableRow {
  department_id: number;//科室id
  name: string;//科室名称
  description: string;//科室描述
  isEditing: boolean; // 是否处于编辑状态
  isNew: boolean;//是否新建
}
const read = true;

// 表格数据
const tableData = ref<TableRow[]>([]);
//获取科室数据
const fetchDepartments = async () => {
  try {
    const response = (await GetDepartmentAll()).data;
    if (response.isSuccess) {
      // 将后端返回的科室数据转换为表格需要的格式
      tableData.value = response.result.map((item: any) => ({
        department_id: item.department_id,//科室id
        name: item.name,//科室名称
        description: item.description,//科室描述
        isEditing: false, // 初始状态为非编辑模式
        isNew: false,//初始状态为非新建模式
      }));
    } else {
      ElMessage.error(response.msg);
    }
  } catch (error) {
    ElMessage.error('获取号源数据失败');
  }
}

//进入编辑模式
const editRow = async (row: TableRow) => {
  row.isEditing = true;
}

//保存修改
const saveRow = async (row: TableRow) => {
  try {
    const data = {
      department_id: row.department_id,
      name: row.name,
      description: row.description
    }
    //调用后端Api更新数据
    const res = (await UpdateDepartment(data)).data;
    if (res.isSuccess) {
      ElMessage.success('保存成功');
      row.isEditing = false;
      row.isNew = false;
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('保存失败');
  }
}

//保存创建
const createRow = async (row: TableRow) => {
  try {
    const data = {
      name: row.name,
      description: row.description
    }
    const res = (await CreateDepartment(data)).data;
    if (res.isSuccess) {
      ElMessage.success('保存成功');
      row.isEditing = false;
      row.isNew = false;
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
    const res = (await DeleteDepartment(row.department_id)).data
    if (res.isSuccess) {
      tableData.value.splice(index, 1);
      ElMessage.success('删除成功');
    } else if (row.isNew) {
      tableData.value.splice(index, 1);
      ElMessage.success('删除成功');
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('删除失败');
  }
}

//添加新行
const onAddItem = () => {
  tableData.value.push({
    department_id: tableData.value.length > 0 ? tableData.value[tableData.value.length - 1].department_id + 1 : 0, // 生成唯一 id
    name: '',//默认科室名称
    description: '',//默认科室描述
    isEditing: false,
    isNew: true,//新添加的行直接进入新建状态
  });
}

onMounted(async () => {
  await fetchDepartments();
})
</script>
