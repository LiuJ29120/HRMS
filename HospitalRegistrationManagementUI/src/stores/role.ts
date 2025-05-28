import { defineStore } from 'pinia'
import { ref } from 'vue'
import router from '@/router/router'

export const useRoleStore = defineStore('role', {
  state: () => ({
    role: -1, // -1 表示未登录
    name: '',
    user_id: -1,
    username:"",
  }),
  actions: {
    checkRole(requiredRole: number) {
      if (this.role === -1) {
        // 如果未登录，跳转到登录页
        router.push('/')
        return false
      }
      if (this.role !== requiredRole) {
        // 如果角色不匹配，跳转到登录页
        router.push('/')
        return false
      }
      return true
    },
  },
  persist: {
    key: 'roleStore', // 自定义存储的键名
    storage: localStorage, // 默认使用 localStorage
  },
})
