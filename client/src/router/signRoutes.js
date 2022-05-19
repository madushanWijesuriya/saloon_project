export const signRoutes = [
  {
    path: '/SignIn',
    name: 'Sign In',
    component: () =>
      import(/* webpackChunkName: "signIn" */ '@/views/Authentications/SignIn'),
    meta: {
      isRequiredAuth: false
    }
  },
  {
    path: '/SignUp',
    name: 'Sign Up',
    component: () =>
      import(/* webpackChunkName: "signUp" */ '@/views/Authentications/SignUp'),
    meta: {
      isRequiredAuth: false
    }
  }
]
