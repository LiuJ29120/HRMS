<template>
  <!--:model="form"：绑定表单数据模型 form。
status-icon：显示表单验证状态的图标。
:rules="rules"：绑定表单验证规则 rules。
ref="ruleFormRef"：为表单组件创建一个引用，方便在脚本中操作表单。
prop：表单项的属性名，用于关联表单数据模型中的字段。
v-model：双向绑定表单数据模型中的字段。
autocomplete="off"：禁用自动填充功能。 -->
  <el-form :model="form" label-width="auto" style="max-width: 600px" status-icon :rules="rules" ref="ruleFormRef">
    <el-form-item label="账号" prop="username">
      <el-input v-model="form.username" autocomplete="off" />
    </el-form-item>
    <el-form-item label="姓名" prop="name">
      <el-input v-model="form.name" autocomplete="off" />
    </el-form-item>
    <el-form-item label="手机号" prop="phone">
      <el-input v-model="form.phone" autocomplete="off" />
    </el-form-item>
    <el-form-item label="性别" prop="gender">
      <el-radio-group v-model="form.gender">
        <el-radio value="1">男</el-radio>
        <el-radio value="2">女</el-radio>
      </el-radio-group>
    </el-form-item>
    <el-form-item label="密码" prop="pass">
      <el-input type="password" v-model="form.pass" autocomplete="off" />
    </el-form-item>
    <el-form-item label="确认密码" prop="checkPass">
      <el-input type="password" v-model="form.checkPass" autocomplete="off" />
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmit">注册</el-button>
      <el-button @click="OpenLogin">返回登录</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>
import { store } from '@/stores/store';
import { nextTick, reactive, ref } from 'vue'
import { ElMessage, type FormInstance, type FormRules } from 'element-plus'
import { Register } from '@/http/Login';

const form = reactive({
  username: '',
  name: '',
  phone: '',
  gender: '',
  pass: '',
  checkPass: '',
})

//ruleFormRef：使用 ref 创建一个表单引用，类型为 FormInstance。
const ruleFormRef = ref<FormInstance>()

const checkUsername = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('请输入账号'))
  }
  callback()
}

const checkName = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('请输入姓名'))
  }
  callback()
}

const checkPhone = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('请输入手机号'))
  }
  setTimeout(() => {
    const reg = /^1[123456789]\d{9}$/;
    if (reg.test(value)) {
      callback();
    } else {
      callback(new Error('请输入正确的手机号'));
    }
  })
}

const checkGender = (rule: any, value: any, callback: any) => {
  if (!value) {
    return callback(new Error('选择性别'))
  }
  callback()
}

const validatePass = (rule: any, value: any, callback: any) => {
  if (value == '') {
    callback(new Error('请输入密码'))
  }
  callback()
}

const validatePass2 = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('请再次输入密码'))
  } else if (value !== form.pass) {
    callback(new Error("两次密码不一致!"))
  } else {
    callback()
  }
}

const rules = reactive<FormRules<typeof form>>({
  username: [{ validator: checkUsername, trigger: 'blur' }],
  name: [{ validator: checkName, trigger: 'blur' }],
  phone: [{ validator: checkPhone, trigger: 'blur' }],
  gender: [{ validator: checkGender, trigger: 'blur' }],
  pass: [{ validator: validatePass, trigger: 'blur' }],
  checkPass: [{ validator: validatePass2, trigger: 'blur' }],
})

const OpenLogin = () => {
  store.CloseRegister()
  store.OpenLogin()
}


const onSubmit = async () => {
  await nextTick(); // 确保 DOM 已经更新
  // console.log(form);
  // console.log(ruleFormRef.value);
  if (!ruleFormRef.value) return;
  try {
    await ruleFormRef.value.validate();
    const data = {
      username: form.username,
      name: form.name,
      phone: form.phone,
      gender: form.gender,
      password: form.pass,
    }
    // console.log(data);
    const res = (await Register(data)).data
    // console.log(res);
    if (res.isSuccess) {
      ElMessage({
        message: "注册成功！",
        type: "success",
      })
      setTimeout(() => {
        OpenLogin();
      }, 2000)
      // const user = res.result
    } else {
      ElMessage.error(res.msg);
    }
  } catch (error) {
    console.error('error submit!', error);
  }
};
</script>
