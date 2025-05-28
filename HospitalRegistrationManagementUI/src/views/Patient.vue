<template>
  <div class="common-layout">
    <el-container>
      <el-header class="header">
        <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" style="flex-basis: 1000px;">
          <el-menu-item index="1" @click="navigateTo('/patient/registration')">挂号选择</el-menu-item>
          <el-menu-item index="2" @click="navigateTo('/patient/registrationInfo')">挂号查询</el-menu-item>
          <el-menu-item index="3" @click="navigateTo('/patient/patientInfo')">个人信息</el-menu-item>
          <el-menu-item index="4" @click="navigateTo('/patient/rating')">就诊评价</el-menu-item>
        </el-menu>
        <!-- 用户信息区域 -->
        <div class="user-info" style="flex-shrink: 0; width: 500px;">
          <el-button @click="dialogVisible = true">系统指引</el-button>
          <el-avatar :size="50">{{ role.name }}</el-avatar>
          <span class="user-name" @click="">{{ role.name }}</span>
          <el-button link @click="logout">退出登录</el-button>
        </div>
      </el-header>
      <el-main> <router-view /></el-main>
      <el-footer>
        <FooterCom />
      </el-footer>
    </el-container>
  </div>
  <el-dialog v-model="dialogVisible" width="1000" :close-on-click-modal="true" center>
    <el-carousel trigger="click" height="500px" :autoplay="false" loop="false">
      <el-carousel-item v-for="item in GuideUrl" :key="item">
        <el-image style="width: 1000px; height: 500px" :src="item.url" fit="contain" />
      </el-carousel-item>
    </el-carousel>
  </el-dialog>
</template>
<script lang="ts" setup>
import FooterCom from '@/components/FooterCom.vue';
import { useRoleStore } from '@/stores/role';
import { ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router';

const router = useRouter()
const route = useRoute()
const activeIndex = ref('1')
const role = useRoleStore()

const dialogVisible = ref(false)

const navigateTo = (path: string) => {
  router.push(path)
}

// 定义一个带有索引签名的接口
interface PathToIndexMap {
  [key: string]: string; // 索引签名
}
// 使用接口定义对象
const PathToIndexMap: PathToIndexMap = {
  '/patient/registration': '1',
  '/patient/registrationInfo': '2',
  '/patient/patientInfo': '3',
  '/patient/rating': '4',
};

const GuideUrl = [{
  value: "a",
  url: '../public/image/guide/1.png'
}, {
  value: "b",
  url: '../public/image/guide/2.png'
}, {
  value: "b",
  url: '../public/image/guide/3.png'
}, {
  value: "b",
  url: '../public/image/guide/4.png'
}, {
  value: "b",
  url: '../public/image/guide/5.png'
}, {
  value: "b",
  url: '../public/image/guide/6.png'
}, {
  value: "b",
  url: '../public/image/guide/7.png'
}, {
  value: "b",
  url: '../public/image/guide/8.png'
}, {
  value: "b",
  url: '../public/image/guide/9.png'
}]

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
