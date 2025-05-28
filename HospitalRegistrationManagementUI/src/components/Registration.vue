<template>
  <el-form :model="form" label-width="auto" style="max-width: 600px">
    <el-form-item label="选择医生">
      <el-select v-model="form.doctor_id" placeholder="Select" style="width: 300px" filterable
        @change="handleDoctorChange">
        <el-option-group v-for="group in groupedDoctors" :key="group.department_id" :label="group.department_name">
          <el-option v-for="doctor in group.doctors" :key="doctor.doctor_id" :label="doctor.name"
            :value="doctor.doctor_id">
            <el-avatar :size="25" :src="doctor.avatar_url" style="float: left;"></el-avatar>
            <span style="float: left;">{{ doctor.name }}</span>
          </el-option>
        </el-option-group>
      </el-select>
    </el-form-item>
    <el-form-item label="选择排班">
      <el-select v-model="form.schedule_id" placeholder="Select schedule" style="width: 300px"
        :disabled="!form.doctor_id">
        <el-option v-for="schedule in filteredSchedules" :key="schedule.schedule_id"
          :label="getShiftType(schedule.shift_type)" :value="schedule.schedule_id"></el-option>
      </el-select>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">挂号</el-button>
      <!-- <el-button>取消</el-button> -->
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>
import { GetDepartmentAll } from '@/http/Department';
import { GetDoctorAll } from '@/http/Doctor';
import { GetPatientByUid } from '@/http/Patient';
import { CreateRegistration } from '@/http/Registration';
import { GetScheduleToday } from '@/http/Schedule';
import { useRoleStore } from '@/stores/role';
import { ElMessage } from 'element-plus';
import { onMounted, reactive, ref } from 'vue'

interface Doctor {
  doctor_id: number;//医生id
  department_id: number;//科室id
  name: string;//医生名称
  title: string;//职称
  avatar_url: string;//头像URL
}

interface Department {
  department_id: number;
  name: string;
}

interface Schedule {
  schedule_id: number,
  doctor_id: number,
  date: string,
  shift_type: number,
  room_number: string,
}

interface GroupedDoctor {
  department_id: number;
  department_name: string;
  doctors: Doctor[];
}

const form = reactive({
  doctor_id: '',
  schedule_id: '',
})

//医生数据
const doctorData = ref<Doctor[]>([]);
// 科室数据
const department = ref<Department[]>([]);
//排班数据
const scheduleData = ref<Schedule[]>([]);
//医生选择组
const groupedDoctors = ref<GroupedDoctor[]>([]);
// 新增排班类型过滤后的数据
const filteredSchedules = ref<Schedule[]>([]);
//班次类型
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

//role状态获取
const role = useRoleStore()
//患者id
let patient_id = -1;

//获取医生数据
const fetchDoctors = async () => {
  try {
    const res = (await GetDoctorAll()).data;
    if (res.isSuccess) {
      // 将后端返回的科室数据转换为表格需要的格式
      doctorData.value = res.result.map((item: any) => ({
        doctor_id: item.doctor_id,//医生id
        department_id: item.department_id,//科室id
        name: item.name,//医生名称
        title: item.title,//职称
        avatar_url: item.avatar_url,//头像URL
      }));
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('获取医生数据失败');
  }
}

// 获取科室数据
const fetchDepartments = async () => {
  try {
    const res = (await GetDepartmentAll()).data;
    if (res.isSuccess) {
      department.value = res.result.map((dept: any) => ({
        department_id: dept.department_id,
        name: dept.name,
      }));
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    ElMessage.error('获取科室数据失败');
  }
}

//获取患者数据
const fetchPatients = async () => {
  try {
    const res = (await GetPatientByUid(role.user_id)).data;
    if (res.isSuccess) {
      patient_id = res.result.patient_id
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error('获取患者数据失败')
  }
}

//获取当日排班数据
const fetchSchedule = async () => {
  try {
    const today = new Date();
    // 设置时间为今天早上8点
    today.setHours(8, 0, 0, 0);
    const res = (await GetScheduleToday(today.toISOString())).data;
    if (res.isSuccess) {
      scheduleData.value = res.result.map((dept: any) => ({
        schedule_id: dept.schedule_id,
        doctor_id: dept.doctor_id,
        date: dept.date,
        shift_type: dept.shift_type,
        room_number: dept.room_number,
      }))
    }
  } catch (error) {
    ElMessage.error("获取排班数据失败")
  }
}

// 当医生选择变化时触发的事件
const handleDoctorChange = (doctorId: number) => {
  // 过滤出选定医生当天的排班
  filteredSchedules.value = scheduleData.value.filter(schedule => schedule.doctor_id === doctorId);
  // 清除已选的排班，因为排班列表已经变化
  form.schedule_id = '';
}


//将医生数据进行整合
const groupDoctorsByDepartment = () => {
  const grouped = department.value.map(dept => ({
    department_id: dept.department_id,
    department_name: dept.name,
    doctors: doctorData.value.filter(doctor => {
      // 过滤出当天有排班的医生
      return scheduleData.value.some(schedule => schedule.doctor_id === doctor.doctor_id &&
        doctor.department_id === dept.department_id);
    })
  }));
  groupedDoctors.value = grouped;
};

// 根据排班类型 ID 获取排班类型
const getShiftType = (id: number) => {
  const dept = shift.find((item) => item.value === id);
  return dept ? dept.label : '未知班次';
};

const onSubmit = async () => {
  try {
    const data = {
      patient_id: patient_id,
      doctor_id: form.doctor_id,
      schedule_id: form.schedule_id,
    };
    const res = (await CreateRegistration(data)).data
    console.log(res);
    if (res.isSuccess) {
      ElMessage.success("已成功挂号")
    } else {
      ElMessage.error(res.result.msg);
    }
  } catch (error) {
    ElMessage.error("挂号失败");
  }
}

onMounted(async () => {
  await fetchDepartments();
  await fetchDoctors();
  await fetchPatients();
  await fetchSchedule();
  groupDoctorsByDepartment();
})

</script>
