import Department from '@/components/Department.vue'
import DoctorManage from '@/components/DoctorManage.vue'
import DoctorWork from '@/components/DoctorWork.vue'
import PatientInfo from '@/components/PatientInfo.vue'
import Rating from '@/components/Rating.vue'
import Registration from '@/components/Registration.vue'
import RegistrationInfo from '@/components/RegistrationInfo.vue'
import RegistrationSources from '@/components/RegistrationSources.vue'
import Schedule from '@/components/Schedule.vue'
import { useRoleStore } from '@/stores/role'
import AdminPanel from '@/views/AdminPanel.vue'
import Doctor from '@/views/Doctor.vue'
import Entrance from '@/views/Entrance.vue'
import Patient from '@/views/Patient.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: Entrance,
    },
    {
      path: '/adminPanel',
      component: AdminPanel,
      children: [
        {
          path: 'home',
          component: RegistrationSources,
          meta: { requiredRole: 0 }, // 管理员角色
        },
        {
          path: 'registrationSources',
          component: RegistrationSources,
          meta: { requiredRole: 0 },
        },
        {
          path: 'department',
          component: Department,
          meta: { requiredRole: 0 },
        },
        {
          path: 'doctor',
          component: DoctorManage,
          meta: { requiredRole: 0 },
        },
        {
          path: 'schedule',
          component: Schedule,
          meta: { requiredRole: 0 },
        },
      ],
    },
    {
      path: '/patient',
      component: Patient,
      children: [
        {
          path: 'home',
          component: Registration,
          meta: { requiredRole: 1 }, // 患者角色
        },
        {
          path: 'registration',
          component: Registration,
          meta: { requiredRole: 1 }, // 患者角色
        },
        {
          path: 'registrationInfo',
          component: RegistrationInfo,
          meta: { requiredRole: 1 }, // 患者角色
        },
        {
          path: 'patientInfo',
          component: PatientInfo,
          meta: { requiredRole: 1 }, // 患者角色
        },
        {
          path: 'rating',
          component: Rating,
          meta: { requiredRole: 1 }, // 患者角色
        },
      ],
    },
    {
      path: '/doctor',
      component: Doctor,
      children: [
        {
          path: 'home',
          component: DoctorWork,
          meta: { requiredRole: 2 },
        },
      ],
    },
  ],
})

// 全局路由守卫
router.beforeEach((to, from, next) => {
  const roleStore = useRoleStore()
  const requiredRole = to.meta.requiredRole // 从路由元信息中获取所需角色

  if (requiredRole !== undefined) {
    if (typeof requiredRole === 'number' && roleStore.checkRole(requiredRole)) {
      next() // 角色匹配，允许访问
    } else {
      next('/') // 角色不匹配，跳转到登录页
    }
  } else {
    next() // 如果没有设置所需角色，直接通过
  }
})

export default router
