import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateSchedule = (prams: {}) => {
  return axios.post(http.value + '/Schedule/CreateSchedule', prams)
}
export const GetScheduleAll = () => {
  return axios.get(http.value + '/Schedule/GetScheduleAll')
}
export const GetScheduleToday = (date: any) => {
  return axios.get(http.value + '/Schedule/GetScheduleToday?date=' + date)
}
export const UpdateSchedule = (prams: {}) => {
  return axios.post(http.value + '/Schedule/UpdateSchedule', prams)
}
export const DeleteSchedule = (id: number) => {
  return axios.get(http.value + '/Schedule/DeleteSchedule?id=' + id)
}
