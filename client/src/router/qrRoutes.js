export const qrRoutes = [
  {
    name: 'QRHome',
    path: '/qrHome',
    component: () =>
      import(/*webpackChunkName: 'QRHome' */ '@/views/QR/QRHome.vue'),
    meta: {
      isRequiredAuth: true
    }
  }
]
