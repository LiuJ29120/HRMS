import axios from 'axios'
import { ref } from 'vue'

const http = ref('http://localhost:5176/api')

export const DownloadReport=async ()=>{
  const response = await axios({
    url: '/Tools/DownloadReport',
    method: 'GET',
    responseType: 'blob', // 重要！
    headers: {
      'Cache-Control': 'no-cache', // 禁用缓存
      'Pragma': 'no-cache',
      'Expires': '0'
    },
  });
  const url_1 = window.URL.createObjectURL(new Blob([response.data]));
  const link = document.createElement('a');
  link.href = url_1;
  link.setAttribute('download', 'Report.xlsx'); // 或者从响应头获取文件名
  document.body.appendChild(link);
  link.click();
}
