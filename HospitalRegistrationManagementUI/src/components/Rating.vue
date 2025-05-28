<template>
  <el-table :data="tableData" style="width: 100%;" max-height="600">
    <el-table-column label="科室名称">
      <template #default="{ row }">
        <span>{{ getDepartmentName(row.registration_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column label="医生名称">
      <template #default="{ row }">
        <span>{{ getDoctorName(row.registration_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column prop="score" label="评分"></el-table-column>
    <el-table-column label="评价">
      <template #default="{ row }">
        <el-input :readonly="true" v-model="row.comment" style="width: 200px" autosize type="textarea" />
      </template>
    </el-table-column>
    <el-table-column label="日期">
      <template #default="{ row }">
        <span>{{ getRegistrationDate(row.registration_id) }}</span>
      </template>
    </el-table-column>
    <el-table-column label="选项">
      <template #default="{ row }">
        <el-button type="primary" @click="modify(tableData.indexOf(row))">修改</el-button>
      </template>
    </el-table-column>
  </el-table>

  <el-dialog v-model="dialogVisible" title="修改评价" width="400" :close-on-click-modal="true" center>
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
      <el-button type="primary" @click="modifySubmit">
        确定
      </el-button>
    </template>
  </el-dialog>
</template>
<script lang="ts" setup>
import { GetDepartmentAll } from '@/http/Department';
import { GetDoctorAll } from '@/http/Doctor';
import { GetPatientByUid } from '@/http/Patient';
import { GetRatingAll, UpdateRating } from '@/http/Rating';
import { GetPatientRegistration } from '@/http/Registration';
import { useRoleStore } from '@/stores/role';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';

interface TableRow {
  rating_id: number,
  registration_id: number,
  score: number,
  comment: string
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

interface Registration {
  registration_id: number,
  patient_id: number,
  doctor_id: number,
  schedule_id: number,
  status: number,
  create_at: number,
}

//表格数据
const tableData = ref<TableRow[]>([])
//科室数据
const departmentData = ref<Department[]>([]);
//医生数据
const doctorData = ref<Doctor[]>([]);
//挂号数据
const registrationData = ref<Registration[]>([]);
//获取role缓存
const role = useRoleStore()

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
      registrationData.value = res.result
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取挂号数据失败")
  }
}

//获取评价数据
const fetchRating = async () => {
  try {
    const res = (await GetRatingAll(patientData.value.patient_id)).data
    if (res.isSuccess) {
      tableData.value = res.result.map((r: any) => ({
        rating_id: r.rating_id,
        registration_id: r.registration_id,
        score: r.score,
        comment: r.comment
      }))
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取评价数据失败")
  }
}


//根据挂号id获取科室名称
const getDepartmentName = (id: number) => {
  const reg = registrationData.value.find(p => p.registration_id === id)
  const doc = doctorData.value.find(p => p.doctor_id === reg?.doctor_id)
  const dept = departmentData.value.find((item) => item.department_id === doc?.department_id)
  return dept ? dept.name : '未知科室'
}

//根据挂号id获取医生名字
const getDoctorName = (id: number) => {
  const reg = registrationData.value.find(p => p.registration_id === id)
  const doc = doctorData.value.find(item => item.doctor_id === reg?.doctor_id)
  return doc ? doc.name : "未知医生"
}

//根据挂号id获取日期
const getRegistrationDate = (id: number) => {
  const reg = registrationData.value.find(item => item.registration_id === id)
  return reg ? reg.create_at.toString().slice(0, 10) : "未知日期"
}

//修改按钮点击事件
const modify = (index: number) => {
  const row = tableData.value[index]
  ratingData.value.registration_id = row.registration_id
  dialogVisible.value = true
}

//修改按钮提交事件
const modifySubmit = async () => {
  try {
    const data = {
      registration_id: ratingData.value.registration_id,
      score: ratingData.value.score,
      comment: ratingData.value.comment,
    }
    const res = (await UpdateRating(data)).data
    if (res.isSuccess) {
      ratingData.value.score = -1
      ratingData.value.comment = ""
      dialogVisible.value = false
      fetchRating()
    } else {
      ElMessage.success(res.msg)
    }
  } catch (error) {
    ElMessage.error("评价失败")
  }
}


onMounted(async () => {
  await fetchPatient()
  await fetchDepartments()
  await fetchDoctors()
  await fetchRegistration()
  await fetchRating()
})
</script>
