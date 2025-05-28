import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateRegistration = (prams: {}) => {
  return axios.post(http.value + '/Registration/CreateRegistration', prams)
}
export const GetDoctorRegistration = (id: number) => {
  return axios.get(http.value + '/Registration/GetDoctorRegistration?id=' + id)
}
export const GetPatientRegistration = (id: number) => {
  return axios.get(http.value + `/Registration/GetPatientRegistration?id=` + id)
}
/**
 * 调用后端的 OverRegistration 方法
 * @param req - RegistrationReq 对象，包含 patient_id, doctor_id, schedule_id 等字段
 * @param status - 操作类型的状态码（如：1表示取消挂号，2表示完成就诊）
 */
export const OverRegistration = (prams: {}, status: number) => {
  return axios.post(http.value + '/Registration/OverRegistration?status=' + status, prams, {
    headers: {
      'Content-Type': 'application/json',
    },
  })
}
