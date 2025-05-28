import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateRegistrationSource = (prams: {}) => {
  return axios.post(http.value + '/RegistrationSource/CreateRegistrationSource', prams)
}
export const GetRegistrationSourceAll = () => {
  return axios.get(http.value + '/RegistrationSource/GetRegistrationSourceAll')
}
export const DeleteRegistrationSource = (id: number) => {
  return axios.get(http.value + `/RegistrationSource/DeleteRegistrationSource?id=${id}`);
}
export const UpdateRegistrationSource = (prams: {}) => {
  return axios.post(http.value + '/RegistrationSource/UpdateRegistrationSource', prams)
}
