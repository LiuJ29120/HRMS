<template>
  <div class="common-layout">
    <el-container>
      <el-header class="header">
        <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" style="flex-basis: 1000px;">
          <el-menu-item index="1" @click="navigateTo('/adminPanel/registrationSources')">号源管理</el-menu-item>
          <el-menu-item index="2" @click="navigateTo('/adminPanel/schedule')">排班管理</el-menu-item>
          <el-menu-item index="3" @click="navigateTo('/adminPanel/doctor')">医生管理</el-menu-item>
          <el-menu-item index="4" @click="navigateTo('/adminPanel/department')">科室管理</el-menu-item>
        </el-menu>
        <!-- 用户信息区域 -->
        <div class="user-info" style="flex-shrink: 0; width: 300px;">
          <el-button type="primary" @click="Report">生成当日报表</el-button>
          <el-avatar :size="50">Admin</el-avatar>
          <span class="user-name">{{ role.name }}</span>
          <el-button link @click="logout">退出登录</el-button>
        </div>
      </el-header>
      <el-main> <router-view /></el-main>
      <el-footer>
        <FooterCom />
      </el-footer>
    </el-container>
  </div>
</template>
<script lang="ts" setup>
import FooterCom from '@/components/FooterCom.vue';
import { DownloadReport } from '@/http/Tool';
import { useRoleStore } from '@/stores/role';
import { ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router';

const router = useRouter()
const route = useRoute()
const activeIndex = ref('1')
const role = useRoleStore()

const navigateTo = (path: string) => {
  router.push(path)
}

// 定义一个带有索引签名的接口
interface PathToIndexMap {
  [key: string]: string; // 索引签名
}
// 使用接口定义对象
const PathToIndexMap: PathToIndexMap = {
  '/adminPanel/home': '1',
  '/adminPanel/schedule': '2',
  '/adminPanel/doctor': '3',
  '/adminPanel/department': '4',
};

// 监听路由变化，更新 activeIndex
watch(() => route.path, (newPath) => {
  activeIndex.value = PathToIndexMap[newPath] || '1'; // 如果没有匹配的路由，则默认为 '1'
}, { immediate: true }); // 立即执行一次以匹配当前路由

const logout = () => {
  // 清空所有 store 状态
  role.$reset();
  // 导航到登录页
  router.push('/');
};

const Report = async () => {
  await DownloadReport();
}
</script>
<style scoped>
.el-container {
  min-height: 100vh;
}
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 20px;
}

.user-name {
  cursor: pointer;
  color: #409eff;
  margin-right: 10px;
}

.el-button {
  margin-left: 10px;
}
</style>
