<template>
  <el-container>
    <el-header class="header">
      <!-- 用户信息区域 -->
      <div class="user-info" style="flex-shrink: 0; width: 300px;">
        <el-avatar :size="50" :src="doctorData.avatar"></el-avatar>
        <span class="user-name">{{ doctorData.name }}</span>
        <el-button link @click="logout">退出登录</el-button>
      </div>
    </el-header>
    <el-main>
      <RouterView />
    </el-main>
    <el-footer>
      <FooterCom />
    </el-footer>
  </el-container>
</template>

<script lang="ts" setup>
import FooterCom from '@/components/FooterCom.vue';
import { GetDoctor } from '@/http/Doctor';
import { useRoleStore } from '@/stores/role';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const role = useRoleStore();

const doctorData = ref({
  avatar: "",
  name: "",
})

const fetchAvatar = async () => {
  try {
    const res = (await GetDoctor(role.user_id)).data;
    if (res.isSuccess) {
      doctorData.value.avatar = res.result.avatar_url;
      doctorData.value.name = res.result.name
    } else {
      ElMessage.error(res.msg)
    }
  } catch (error) {
    ElMessage.error("获取医生数据失败")
  }
}

const logout = () => {
  // 清空所有 store 状态
  role.$reset();
  // 导航到登录页
  router.push('/');
};

onMounted(async () => {
  await fetchAvatar()
})

</script>
<style scoped>
.el-container {
  min-height: 100vh;
}
.header {
  display: flex;
  justify-content: flex-end;
  /* 更改为这个属性以将内容推到右侧 */
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
