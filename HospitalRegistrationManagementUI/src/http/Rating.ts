import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const CreateRation = (prams: {}) => {
  return axios.post(http.value + '/Rating/CreateRation', prams)
}
export const GetRatingAll = (id:number) => {
  return axios.get(http.value + '/Rating/GetRatingAll?id='+id)
}
export const UpdateRating = (prams: {}) => {
  return axios.post(http.value + '/Rating/UpdateRating', prams)
}
export const DeleteRating = (id: number) => {
  return axios.get(http.value + '/Rating/DeleteRating?id='+id)
}
