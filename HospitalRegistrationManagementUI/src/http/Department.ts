import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateDepartment = (prams: {}) => {
  return axios.post(http.value + '/Department/CreateDepartment', prams)
}
export const GetDepartmentAll = () => {
  return axios.get(http.value + '/Department/GetDepartmentAll')
}
export const UpdateDepartment = (prams: {}) => {
  return axios.post(http.value + '/Department/UpdateDepartment', prams)
}
export const DeleteDepartment = (id: number) => {
  return axios.get(http.value + '/Department/DeleteDepartment?id='+id)
}
