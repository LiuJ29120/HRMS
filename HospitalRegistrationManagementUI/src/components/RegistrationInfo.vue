<template>
  <el-table :data="tableData" style="width: 100%;" max-height=700>
    <el-table-column prop="doctor_id" label="科室名称">
      <template #default="{ row }">
        <span>{{ getDepartmentName(row.doctor_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="doctor_id" label="医生名称">
      <template #default="{ row }">
        <span>{{ getDoctorName(row.doctor_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="date" label="日期"></el-table-column>
    <el-table-column prop="schedule_id" label="时间">
      <template #default="{ row }">
        <span>{{ getScheduleShift(row.schedule_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="status" label="状态"></el-table-column>
    <el-table-column fixed="right" label="选项">
      <template #default="{ row }">
        <el-button v-if="row.isCancel" type="danger" @click="cancel(tableData.indexOf(row))">取消</el-button>
        <el-button v-if="row.isRating" type="primary" @click="comment(tableData.indexOf(row))">评价</el-button>
      </template>
    </el-table-column>
  </el-table>

  <el-dialog v-model="dialogVisible" title="评价" width="500" :close-on-click-modal="true" center>
    <el-form :model="ratingData" label-width="auto">
      <el-form-item label="评分">
        <el-rate v-model="ratingData.score" :texts="rateText" show-text></el-rate>
      </el-form-item>
      <el-form-item label="评价">
        <el-input v-model="ratingData.comment" placeholder="请输入您的评价" type="textarea" autosize></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="dialogVisible = false">取消</el-button>
      <el-button type="primary" @click="commentSubmit">
        确定
      </el-button>
    </template>
  </el-dialog>
</template>
<script lang="ts" setup>
import { GetDepartmentAll } from '@/http/Department';
import { GetDoctorAll } from '@/http/Doctor';
import { GetPatientByUid } from '@/http/Patient';
import { CreateRation } from '@/http/Rating';
import { GetPatientRegistration, OverRegistration } from '@/http/Registration';
import { GetScheduleAll } from '@/http/Schedule';
import { useRoleStore } from '@/stores/role';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';

interface TableRow {
  registration_id: number,
  doctor_id: number,//医生id
  status: string,//状态
  date: string,//时间
  schedule_id: number,//排班id
  isCancel: boolean,//是否处于待就诊状态
  isRating: boolean,//是否处于已就诊状态
}

interface Department {
  department_id: number;//科室id
  name: string;//科室名称
  description: string;//科室描述
}

interface Doctor {
  doctor_id: number;//医生id
  user_id: number;//用户id
  department_id: number;//科室id
  name: string;//医生名称
  title: string;//职称
  bio: string;//简介
  avatar_url: string;//头像URL
}

interface Schedule {
  schedule_id: number,//排班id
  doctor_id: number,//医生id
  date: string,//排班日期
  shift_type: number,//班次类型
  room_number: string,//诊室号
}

// 表格数据
const tableData = ref<TableRow[]>([]);
//科室数据
const departmentData = ref<Department[]>([]);
//医生数据
const doctorData = ref<Doctor[]>([]);
//排班数据
const scheduleData = ref<Schedule[]>([]);
//患者数据
const patientData = ref({
  patient_id: -1,
  user_id: -1,
  name: "",
  gender: -1,
  phone: "",
})
//评价数据
const ratingData = ref({
  registration_id: -1,
  score: -1,
  comment: "",
})
//dialog是否显示
const dialogVisible = ref(false)

//获取role缓存
const role = useRoleStore()
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
//状态
const status = [
  {
    value: 0,
    label: "待就诊",
    cancel: true,
    rating: false,
  }, {
    value: 1,
    label: "已取消",
    cancel: false,
    rating: false,
  }, {
    value: 2,
    label: "已完成",
    cancel: false,
    rating: true,
  },
  {
    value: 3,
    label: "已评价",
    cancel: false,
    rating: false,
  }
]
//评分文字显示
const rateText = [
  "很差",
  "差",
  "正常",
  "良好",
  "优秀",
]

//获取科室数据
const fetchDepartments = async () => {
  try {
    const res = (await GetDepartmentAll()).data;
    if (res.isSuccess) {
      departmentData.value = res.result
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取科室数据失败");
  }
}

//获取医生数据
const fetchDoctors = async () => {
  try {
    const res = (await GetDoctorAll()).data;
    if (res.isSuccess) {
      doctorData.value = res.result
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取医生数据失败")
  }
}

//获取排班数据
const fetchSchedules = async () => {
  try {
    const res = (await GetScheduleAll()).data
    if (res.isSuccess) {
      scheduleData.value = res.result
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取排班数据失败")
  }
}

//获取患者数据
const fetchPatient = async () => {
  try {
    const res = (await GetPatientByUid(role.user_id)).data
    if (res.isSuccess) {
      patientData.value = res.result
    } else {
      ElMessage.error(res.msg)
    }
  } catch {
    ElMessage.error("获取患者数据失败")
  }
}

//获取挂号数据
const fetchRegistration = async () => {
  try {
    const res = (await GetPatientRegistration(patientData.value.patient_id)).data
    if (res.isSuccess) {
      tableData.value = res.result.map((item: any) => ({
        registration_id: item.registration_id,
        doctor_id: item.doctor_id,
        status: getStatusName(item.status),
        date: item.create_at.toString().slice(0, 10),
        schedule_id: item.schedule_id,
        isCancel: getStatusCancel(item.status),
        isRating: getStatusRating(item.status),
      }))
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取挂号数据失败")
  }
}

//根据医生id获取科室名称
const getDepartmentName = (id: number) => {
  const doc = doctorData.value.find(p => p.doctor_id === id)
  const dept = departmentData.value.find((item) => item.department_id === doc?.department_id)
  return dept ? dept.name : '未知科室'
}

//根据医生id获取医生名字
const getDoctorName = (id: number) => {
  const doc = doctorData.value.find(item => item.doctor_id === id)
  return doc ? doc.name : "未知医生"
}

//根据排班id获取就诊时间
const getScheduleShift = (id: number) => {
  const sch = scheduleData.value.find(p => p.schedule_id === id)
  const shift_type = shift.find(p => p.value === sch?.shift_type)
  return shift_type ? shift_type.label : "时间已过"
}

//根据状态id获取状态名称
const getStatusName = (id: number) => {
  const sta = status.find(p => p.value === id);
  return sta ? sta.label : "未知状态"
}

//根据状态id获取"取消"按钮状态
const getStatusCancel = (id: number) => {
  const sta = status.find(p => p.value === id);
  return sta ? sta.cancel : false
}

//根据状态id获取 "评价" 按钮状态
const getStatusRating = (id: number) => {
  const sta = status.find(p => p.value === id);
  return sta ? sta.rating : false
}

//取消挂号
const cancel = async (index: number) => {
  try {
    const row = tableData.value[index];
    const data = {
      patient_id: patientData.value.patient_id,
      doctor_id: row.doctor_id,
      schedule_id: row.schedule_id,
    }
    const res = (await OverRegistration(data, 1)).data;
    if (res.isSuccess) {
      fetchRegistration()
      console.log(res);
      ElMessage.success("取消挂号成功")
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("取消挂号失败")
  }
}

//评价按钮点击事件
const comment = (index: number) => {
  const row = tableData.value[index]
  ratingData.value.registration_id = row.registration_id
  dialogVisible.value = true;
}

//评价按钮提交事件
const commentSubmit = async () => {
  try {
    const data = {
      registration_id: ratingData.value.registration_id,
      score: ratingData.value.score,
      comment: ratingData.value.comment,
    }
    const res = (await CreateRation(data)).data
    if (res.isSuccess) {
      ratingData.value.score = -1
      ratingData.value.comment = ""
      dialogVisible.value = false
      fetchRegistration()
    } else {
      ElMessage.success(res.msg)
    }
  } catch (error) {
    ElMessage.error("评价失败")
  }
}

onMounted(async () => {
  await fetchDepartments();
  await fetchDoctors();
  await fetchSchedules();
  await fetchPatient();
  await fetchRegistration();
})
</script>
