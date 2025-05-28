import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const UpdatePatient = (prams: {}) => {
  return axios.post(http.value + '/Patient/UpdatePatient', prams)
}
export const DeletePatient = (id: number) => {
  return axios.get(http.value + '/Patient/DeletePatient?id=' + id)
}
export const GetPatientByUid = (id: number) => {
  return axios.get(http.value + '/Patient/GetPatientByUid?id=' + id)
}
export const GetPatientByPid = (id: number) => {
  return axios.get(http.value + '/Patient/GetPatientByPid?id=' + id)
}
export const UpdatePassword = (prams: {}, pass: string) => {
  return axios.post(http.value + '/User/UpdatePassword?pass=' + pass, prams)
}
