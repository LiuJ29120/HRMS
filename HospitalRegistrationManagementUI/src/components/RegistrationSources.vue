<template>
  <el-table :data="tableData" style="width: 100%" max-height="700">
    <el-table-column fixed prop="id" label="号源id" width="150" />
    <el-table-column prop="department_id" label="科室id" width="200">
      <template #default="{ row }">
        <el-select v-if="row.isNew" v-model="row.department_id" placeholder="选择科室" size="large" style="width: 120px">
          <el-option v-for="item in department" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
        <span v-else>
          {{ getDepartmentName(row.department_id) }}
        </span>
      </template>
    </el-table-column>
    <el-table-column prop="date" label="号源日期" width="300">
      <template #default="{ row }">
        <el-date-picker v-if="row.isEditing || row.isNew" v-model="row.date" type="date" placeholder="选择日期"
          size="small" />
        <span v-else>{{ formatDate(row.date) }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="available_slots" label="剩余可挂号数" width="120" />
    <el-table-column prop="total" label="总号源数" width="600">
      <template #default="{ row }">
        <el-input v-if="row.isEditing || row.isNew" v-model="row.total" style="width: 240px" placeholder="请输入总号源数" />
        <span v-else>{{ row.total }}</span>
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

<script lang="ts">
import { ref, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { GetDepartmentAll } from '@/http/Department';
import { CreateRegistrationSource, DeleteRegistrationSource, GetRegistrationSourceAll, UpdateRegistrationSource } from '@/http/RegistrationSource';

interface TableRow {
  id: number; // 号源id
  department_id: number; // 科室id
  date: string; // 号源日期
  available_slots: number; // 剩余可挂号数
  total: number; // 总号源数
  isEditing: boolean; // 是否处于编辑状态
  isNew: boolean;//是否新建
}

interface Department {
  value: number; // 科室id
  label: string; // 科室名称
}

export default {
  setup() {
    // 表格数据
    const tableData = ref<TableRow[]>([]);

    // 科室数据
    const department = ref<Department[]>([]);

    const fetchRegistrationSources = async () => {
      try {
        const response = (await GetRegistrationSourceAll()).data;
        if (response.isSuccess) {
          // 将后端返回的号源数据转换为表格需要的格式
          tableData.value = response.result.map((item: any) => ({
            id: item.source_id, // 号源id
            department_id: item.department_id, // 科室id
            date: item.date, // 号源日期
            available_slots: item.available_slots, // 剩余可挂号数
            total: item.total_slots, // 总号源数
            isEditing: false, // 初始状态为非编辑模式
            isNew: false,//初始状态为非新建模式
          }));
        } else {
          ElMessage.error(response.msg);
        }
      } catch (error) {
        ElMessage.error('获取号源数据失败');
      }
    };

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

    // 进入编辑模式
    const editRow = (row: TableRow) => {
      row.isEditing = true;
    };

    // 保存修改
    const saveRow = async (row: TableRow) => {
      try {
        // 将日期加8小时
        const dateWith8Hours = new Date(row.date);
        dateWith8Hours.setHours(dateWith8Hours.getHours() + 8);
        const data = {
          source_id: row.id,
          department_id: row.department_id, // 发送科室 ID
          date: dateWith8Hours,
          total_slots: row.total,
        }
        console.log(data)
        // 调用后端 API 更新数据
        const res = (await UpdateRegistrationSource(data)).data;
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
    };
    //保存创建
    const createRow = async (row: TableRow) => {
      try {
        // 将日期加8小时
        const dateWith8Hours = new Date(row.date);
        dateWith8Hours.setHours(dateWith8Hours.getHours() + 8);
        const data = {
          source_id: row.id,
          department_id: row.department_id, // 发送科室 ID
          date: dateWith8Hours,
          total_slots: row.total,
        }
        const res = (await CreateRegistrationSource(data)).data;
        // row.available_slots = row.total
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
    };

    // 删除行
    const deleteRow = async (index: number) => {
      try {
        const row = tableData.value[index];
        // console.log(row.id)
        const res = (await DeleteRegistrationSource(row.id)).data;
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
    };

    // 添加新行
    const onAddItem = () => {
      tableData.value.push({
        id: tableData.value.length > 0 ? tableData.value[tableData.value.length - 1].id + 1 : 0, // 生成唯一 id
        department_id: 0, // 默认科室id
        date: '', // 默认日期
        available_slots: 0, // 默认剩余可挂号数
        total: 0, // 默认总号源数
        isEditing: false,
        isNew: true,//新添加的行直接进入新建状态
      });
    };
    const formatDate = (dateString: string): string => {
      if (!dateString) return ''; // 如果日期为空，返回空字符串
      const date = new Date(dateString);
      date.setHours(date.getDate() + 8)
      return date.toISOString().slice(0, 10); // 截取前10个字符，即 "YYYY-MM-DD"
    };
    // 初始化时获取科室数据
    onMounted(async () => {
      await fetchDepartments();
      await fetchRegistrationSources();
    });

    return {
      tableData,
      department,
      getDepartmentName,
      editRow,
      saveRow,
      deleteRow,
      onAddItem,
      createRow,
      formatDate,
    };
  },
};

</script>
