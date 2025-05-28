import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const UpAvartar = (file: File) => {
  const formData = new FormData()
  formData.append('file', file)
  return axios.post(http.value + '/Upload/SetAvatar', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
  })
}
