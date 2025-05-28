<template>
  <el-form :model="form" label-width="auto" style="max-width: 600px; margin: 0 auto;">
    <el-form-item label="账号">
      <el-input v-model="form.username" />
    </el-form-item>
    <el-form-item label="密码">
      <el-input type="password" v-model="form.pass" />
    </el-form-item>
    <div class="btn-container">
      <el-button type="primary" @click="onSubmit">登录</el-button>
      <el-button @click="OpenRegister">注册</el-button>
    </div>
  </el-form>
  <!-- <el-button type="primary" text>忘记密码</el-button> -->
</template>

<script lang="ts" setup>
import { LoginOn } from '@/http/Login';
import router from '@/router/router';
import { useRoleStore } from '@/stores/role';
import { store } from '@/stores/store';
import { ElMessage } from 'element-plus';
import { reactive } from 'vue'

const role = useRoleStore();

const form = reactive({
  username: '',
  pass: '',
})

const OpenRegister = () => {
  store.CloseLogin();
  store.OpenRegister()
}

const onSubmit = async () => {
  const data = {
    username: form.username,
    password: form.pass,
  }
  const res = (await LoginOn(data)).data
  if (res.isSuccess) {
    ElMessage({
      message: '登陆成功',
      type: 'success'
    })
    const user = res.result;
    role.name = user.name;
    role.role = user.role;
    role.username = user.username
    role.user_id = res.result.user_id
    if (user.role === 0) {
      role.name = 'admin';
      router.push('/adminPanel/home')
    } else if (user.role === 1) {
      router.push('/patient/home')
    } else if (user.role === 2) {
      router.push('/doctor/home')
    }
  } else {
    ElMessage.error(res.msg);
  }
}
</script>
<style lang="scss" scoped>
.btn-container {
  display: flex;
  justify-content: center;
  /* 水平居中按钮 */
}
</style>
