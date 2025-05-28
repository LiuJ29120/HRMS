<template>
  <el-table :data="tableData" style="width: 100%;" max-height="600">
    <el-table-column prop="registration_id" label="挂号id"></el-table-column>
    <el-table-column prop="patient_id" label="患者id"></el-table-column>
    <el-table-column prop="patient_name" label="患者姓名"></el-table-column>
    <el-table-column prop="data" label="挂号日期"></el-table-column>
    <el-table-column prop="status" label="挂号状态">
      <template #default="{ row }">
        <span>{{ getStatusName(row.status) }}</span>
      </template>
    </el-table-column>
    <el-table-column label="选项">
      <template #default="{ row }">
        <el-button type="primary" @click="diagnose(row), dialogVisible = true">诊断</el-button>
      </template>
    </el-table-column>
  </el-table>

  <el-dialog v-model="dialogVisible" title="诊断" width="500" :close-on-click-modal="true" center>
    <el-form :model="MedicalRecordData" label-width="auto">
      <el-form-item label="诊断结果">
        <el-input v-model="MedicalRecordData.diagnosis" placeholder="请输入诊断结果" type="textarea" autosize></el-input>
      </el-form-item>
      <el-form-item label="处方信息">
        <el-input v-model="MedicalRecordData.prescription" placeholder="请输入处方信息" type="textarea" autosize></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="dialogVisible = false">取消</el-button>
      <el-button type="primary" @click="diagnoseSubmit">
        确定
      </el-button>
    </template>
  </el-dialog>
</template>
<script lang="ts" setup>
import { GetDoctor } from '@/http/Doctor';
import { CreateMedicalRecord } from '@/http/MedicalRecord';
import { GetPatientByPid, GetPatientByUid } from '@/http/Patient';
import { GetDoctorRegistration, OverRegistration } from '@/http/Registration';
import { UpdateRegistrationSource } from '@/http/RegistrationSource';
import { useRoleStore } from '@/stores/role';
import { ElMessage, type Table } from 'element-plus';
import { onMounted, ref } from 'vue';

interface TableRow {
  registration_id: number,
  patient_id: number,
  patient_name: string,
  doctor_id: number,
  schedule_id: number,
  status: number,
  data: string,
}

const tableData = ref<TableRow[]>([])
const role = useRoleStore()
const doctorData = ref({
  doctor_id: -1,
})
const dialogVisible = ref(false);
let RegistrationData = {
  registration_id: -1,
  patient_id: -1,
  doctor_id: -1,
  schedule_id: -1,
}

const MedicalRecordData = ref({
  diagnosis: "",
  prescription: "",
})
//状态
const status = [
  {
    value: 0,
    label: "待就诊",
  }, {
    value: 1,
    label: "已取消",
  }, {
    value: 2,
    label: "已完成",
  }
]

//获取医生数据
const fetchDoctor = async () => {
  try {
    const res = (await GetDoctor(role.user_id)).data;
    if (res.isSuccess) {
      doctorData.value.doctor_id = res.result.doctor_id;
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取医生数据失败")
  }
}

//获取挂号数据
const fetchRegistration = async () => {
  try {
    const res = (await GetDoctorRegistration(doctorData.value.doctor_id)).data;
    if (res.isSuccess) {
      // 使用 Promise.all 等待所有的患者姓名 Promise 完成
      const patientNamesPromises = res.result.map(async (item: any) => {
        const patientName = await getPatientName(item.patient_id);
        return {
          registration_id: item.registration_id,
          patient_id: item.patient_id,
          patient_name: patientName,
          doctor_id: item.doctor_id,
          schedule_id: item.schedule_id,
          status: item.status,
          data: item.create_at.toString().slice(0, 10),
        };
      });
      // 等待所有的 Promise 完成
      const patientNamesData = await Promise.all(patientNamesPromises);
      tableData.value = patientNamesData;
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取挂号数据失败")
  }
}

//获取患者姓名
const getPatientName = async (id: number) => {
  let name = ""
  try {
    const res = (await GetPatientByPid(id)).data;
    if (res.isSuccess) {
      name = res.result.name
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取患者姓名失败")
  }
  return name
}

//根据状态id获取状态名称
const getStatusName = (id: number) => {
  const sta = status.find(p => p.value === id);
  return sta ? sta.label : "未知状态"
}

const diagnose = (row: TableRow) => {
  RegistrationData.registration_id = row.registration_id;
  RegistrationData.patient_id = row.patient_id;
  RegistrationData.doctor_id = row.doctor_id;
  RegistrationData.schedule_id = row.schedule_id;
}

const diagnoseSubmit = async () => {
  try {
    const medicalRecordData = {
      registration_id: RegistrationData.registration_id,
      diagnosis: MedicalRecordData.value.diagnosis,
      prescription: MedicalRecordData.value.prescription,
    }
    const MedicalRecordRes = (await CreateMedicalRecord(medicalRecordData)).data;
    if (!MedicalRecordRes.isSuccess) {
      ElMessage.error(MedicalRecordRes.msg)
      return;
    } else {
      ElMessage.success(MedicalRecordRes.msg)
    }
    const registrationData = {
      patient_id: RegistrationData.patient_id,
      doctor_id: RegistrationData.doctor_id,
      schedule_id: RegistrationData.schedule_id,
    }
    const RegistrationRes = (await OverRegistration(registrationData, 2)).data
    if (!RegistrationRes.isSuccess) {
      ElMessage.error(RegistrationRes.msg)
      return
    } else {
      ElMessage.success(RegistrationRes.msg)
    }
    dialogVisible.value = false
    fetchRegistration()
  } catch (error) {
    ElMessage.error("诊断提交失败")
  }
}

onMounted(async () => {
  await fetchDoctor()
  await fetchRegistration()
})
</script>
