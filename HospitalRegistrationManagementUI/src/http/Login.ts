import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const LoginOn = (prams: {}) => {
  return axios.post(http.value + '/Login/LoginOn', prams)
}
export const Register = (prams: {}) => {
  return axios.post(http.value + '/Login/Register', prams)
}
