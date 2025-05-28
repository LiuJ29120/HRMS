<template>
  <el-table :data="tableData" style="width: 100; max-height: 700;">
    <el-table-column prop="schedule_id" fixed label="排班id"></el-table-column>
    <el-table-column label="医生id">
      <template #default="{ row }">
        <el-select v-if="row.isEditing" v-model="row.doctor_id" placeholder="选择医生" size="large" style="width: 150px">
          <el-option v-for="item in doctor" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
        <span v-else>
          {{ getDoctorName(row.doctor_id) }}
        </span>
      </template>
    </el-table-column>
    <el-table-column label="排班日期">
      <template #default="{ row }">
        <el-date-picker v-if="row.isEditing" v-model="row.date" type="date" placeholder="选择日期" size="small" />
        <span v-else>{{ formatDate(row.date) }}</span>
      </template>
    </el-table-column>
    <el-table-column label="班次类型">
      <template #default="{ row }">
        <el-select v-if="row.isEditing" v-model="row.shift_type" placeholder="选择班次" size="large" style="width: 150px">
          <el-option v-for="item in shift" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
        <span v-else>
          {{ getShiftType(row.shift_type) }}
        </span>
      </template>
    </el-table-column>
    <el-table-column label="诊室号">
      <template #default="{ row }">
        <el-input v-if="row.isEditing" v-model="row.room_number" style="width: 150px" placeholder="请输入诊室号" />
        <span v-else>{{ row.room_number }}</span>
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
        <el-button link type="danger" size="small" @click="deleteRow(tableData.indexOf(row))">
          删除
        </el-button>
      </template>
    </el-table-column>
  </el-table>
  <el-button class="mt-4" style="width: 100%" @click="dialogVisible = true">
    新增
  </el-button>


  <el-dialog v-model="dialogVisible" title="新增排班" width="500" :close-on-click-modal="true" center>
    <el-form :model="CSchedule" label-width="auto">
      <el-form-item label="医生名:">
        <el-select v-model="CSchedule.doctor_id" placeholder="医生选择" size="large" style="width: 300px">
          <el-option v-for="item in doctor" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
      </el-form-item>
      <el-form-item label="排班日期:">
        <el-date-picker v-model="CSchedule.date" type="date" placeholder="选择日期" size="default" />
      </el-form-item>
      <el-form-item label="班次类型:">
        <el-select v-model="CSchedule.shift_type" placeholder="选择班次" size="large" style="width: 300px">
          <el-option v-for="item in shift" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
      </el-form-item>
      <el-form-item label="诊室号:">
        <el-input v-model="CSchedule.room_number" style="width: 300px" autosize placeholder="请输入诊室号"></el-input>
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
import { GetDoctorAll } from '@/http/Doctor';
import { CreateSchedule, DeleteSchedule, GetScheduleAll, UpdateSchedule } from '@/http/Schedule';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';

interface TableRow {
  schedule_id: number,//排班id
  doctor_id: number,//医生id
  date: string,//排班日期
  shift_type: number,//班次类型
  room_number: string,//诊室号
  isEditing: boolean; // 是否处于编辑状态
}

interface Doctor {
  value: number; // 医生id
  label: string; // 医生姓名
}

const CSchedule = ref({
  doctor_id: '',//医生id
  date: '',//排班日期
  shift_type: '',//班次类型
  room_number: '',//诊室号
})

const shift = [
  {
    value: 0,
    label: "上午"
  },
  {
    value: 1,
    label: "下午"
  }
]

// 表格数据
const tableData = ref<TableRow[]>([]);
// 科室数据
const doctor = ref<Doctor[]>([]);

const dialogVisible = ref(false)
//获取科室数据
const fetchSchedules = async () => {
  try {
    const response = (await GetScheduleAll()).data;
    if (response.isSuccess) {
      // 将后端返回的科室数据转换为表格需要的格式
      tableData.value = response.result.map((item: any) => ({
        schedule_id: item.schedule_id,//排班id
        doctor_id: item.doctor_id,//医生id
        date: item.date,//排班日期
        shift_type: item.shift_type,//班次类型
        room_number: item.room_number,//诊室号
        isEditing: false, // 初始状态为非编辑模式
      }));
    } else {
      ElMessage.error(response.msg);
    }
  } catch (error) {
    ElMessage.error('获取号源数据失败');
  }
}

//获取医生数据
const fetchDoctors = async () => {
  try {
    const response = (await GetDoctorAll()).data;
    if (response.isSuccess) {
      // 将后端返回的科室数据转换为表格需要的格式
      doctor.value = response.result.map((item: any) => ({
        value: item.doctor_id,//医生id
        label: item.name,//医生名称
      }));
    } else {
      ElMessage.error(response.msg);
    }
  } catch (error) {
    ElMessage.error('获取医生数据失败');
  }
}

// 根据医生 ID 获取医生名称
const getDoctorName = (doctor_id: number) => {
  const dept = doctor.value.find((item) => item.value === doctor_id);
  return dept ? dept.label : '未知医生';
};

// 根据排班类型 ID 获取排班类型
const getShiftType = (id: number) => {
  const dept = shift.find((item) => item.value === id);
  return dept ? dept.label : '未知班次';
};

const formatDate = (dateString: string): string => {
  if (!dateString) return ''; // 如果日期为空，返回空字符串
  const date = new Date(dateString);
  date.setHours(date.getDate() + 8)
  return date.toISOString().slice(0, 10); // 截取前10个字符，即 "YYYY-MM-DD"
};

//进入编辑模式
const editRow = async (row: TableRow) => {
  row.isEditing = true;
}
//保存修改
const saveRow = async (row: TableRow) => {
  // 将日期加8小时
  const dateWith8Hours = new Date(row.date);
  dateWith8Hours.setHours(dateWith8Hours.getHours() + 8);
  try {
    const data = {
      schedule_id: row.schedule_id,
      doctor_id: row.doctor_id,
      date: dateWith8Hours,
      shift_type: row.shift_type,
      room_number: row.room_number
    }
    //调用后端Api更新数据
    const res = (await UpdateSchedule(data)).data;
    if (res.isSuccess) {
      ElMessage.success('保存成功');
      row.isEditing = false;
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
    const res = (await DeleteSchedule(row.schedule_id)).data
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


//创建
const CreateDocForm = async () => {
  // 将日期加8小时
  const dateWith8Hours = new Date(CSchedule.value.date);
  dateWith8Hours.setHours(dateWith8Hours.getHours() + 8);
  try {
    const data = {
      doctor_id: CSchedule.value.doctor_id,//医生id
      date: dateWith8Hours,//排版日期
      shift_type: CSchedule.value.shift_type,//班次类型
      room_number: CSchedule.value.room_number,//诊室号
    };

    const res = (await CreateSchedule(data)).data;

    if (res.isSuccess) {
      ElMessage.success('保存成功');
      // 重置CDoctor对象为初始状态
      CSchedule.value = {
        doctor_id: '',//医生id
        date: '',//排班日期
        shift_type: '',//班次类型
        room_number: '',//诊室号
      };
      dialogVisible.value = false;

      // 更新数据
      await fetchSchedules();
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('保存失败');
  }
}

onMounted(async () => {
  await fetchSchedules();
  await fetchDoctors();
})
</script>
