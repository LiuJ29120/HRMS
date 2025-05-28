import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateDoctor = (prams: {}) => {
  return axios.post(http.value + '/Doctor/CreateDoctor', prams)
}
export const GetDoctorAll = () => {
  return axios.get(http.value + '/Doctor/GetDoctorAll')
}
export const UpdateDoctor = (prams: {}) => {
  return axios.post(http.value + '/Doctor/UpdateDoctor', prams)
}
export const DeleteDoctor = (id: number) => {
  return axios.get(http.value + '/Doctor/DeleteDoctor?id=' + id)
}
export const GetDoctor = (id: number) => {
  return axios.get(http.value + '/Doctor/GetDoctor?id=' + id)
}
