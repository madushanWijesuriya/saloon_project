import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Others/Home.vue'
import { signRoutes } from '@/router/signRoutes'
import store from '@/store'
import { productRoutes } from '@/router/productRoutes'
import { appointmentRoutes } from '@/router/appointmentRoutes'
import { qrRoutes } from '@/router/qrRoutes'
import { reportRoutes } from '@/router/reportRoutes'
import { orderRoutes } from '@/router/orderRoutes'

Vue.use(VueRouter)

const routes = [
  ...signRoutes,
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta: {
      isRequiredAuth: true
    }
  },
  {
    path: '/about',
    name: 'About',
    component: () =>
      import(/* webpackChunkName: "about" */ '@/views/Others/About.vue'),
    meta: {
      isRequiredAuth: false
    }
  },
  ...productRoutes,
  ...appointmentRoutes,
  ...qrRoutes,
  ...reportRoutes,
  ...orderRoutes
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  if (to.path === '/signIn' || to.path === 'signUp') {
    store.commit('setIsMenuHide', true)
  } else {
    store.commit('setIsMenuHide', false)
  }
  const userLoggedIn = store.state.user.user
  const isRequiredAuth = to.matched.some(record => record.meta.isRequiredAuth)
  if ((to.path === '/signIn' || to.path === '/signUp') && !userLoggedIn) {
    next()
  } else if (isRequiredAuth && userLoggedIn) {
    next()
  } else if (!isRequiredAuth) {
    next()
  } else if (isRequiredAuth && !userLoggedIn) {
    next({
      path: '/signIn'
    })
  }
})

export default router
