import { reactive } from "vue";

export const store = reactive({
  IsShowLogin:true,
  IsShowRegister:false,
  OpenLogin(){
    this.IsShowLogin = true
  },
  CloseLogin(){
    this.IsShowLogin = false
  },
  OpenRegister(){
    this.IsShowRegister = true
  },
  CloseRegister(){
    this.IsShowRegister = false
  }
})
