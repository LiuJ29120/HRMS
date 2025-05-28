import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateMedicalRecord = (prams: {}) => {
  return axios.post(http.value + '/MedicalRecord/CreateMedicalRecord', prams)
}
export const GetMedicalRecordByPid = (id: number) => {
  return axios.get(http.value + '/MedicalRecord/GetMedicalRecordByPid?id+' + id)
}
export const UpdateMedicalRecord = (prams: {}) => {
  return axios.post(http.value + '/MedicalRecord/UpdateMedicalRecord', prams)
}
export const DeleteMedicalRecord = (id: number) => {
  return axios.get(http.value + '/MedicalRecord/DeleteMedicalRecord?id=' + id)
}
