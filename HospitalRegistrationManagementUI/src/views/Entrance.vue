<template>
  <div class="container">
    <div class="login-box">
      <el-card style="max-width: 480px;margin: auto;">
        <template #header>
          <div class="card-header">
            <span v-if="store.IsShowLogin">基于.net的医院挂号管理系统设计与实现登录</span>
            <span v-if="store.IsShowRegister">基于.net的医院挂号管理系统设计与实现注册</span>
          </div>
        </template>
        <Login v-if="store.IsShowLogin" />
        <Register v-if="store.IsShowRegister" />
      </el-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { store } from '@/stores/store';
import Login from '../components/Login.vue'
import Register from '@/components/Register.vue';
</script>
<style lang="scss">
.container {
  display: flex;
  justify-content: center;
  /* 水平居中 */
  align-items: center;
  /* 垂直居中 */
  height: 100vh;
  /* 容器高度设为视口高度 */
  // background-color: #243aca11;
  background-image: url('../public/image/background/login1.jpg');
}

.login-box {
  width: 500px;
  // height: 200px;
  /* 其他样式 */
  text-align: center;
}

</style>
