export const reportRoutes = [
  {
    path: '/reports',
    name: 'Reports',
    component: () =>
      import(/* webpackChunkName: "Reports" */ '@/views/Reports/Reports'),
    meta: {
      isRequiredAuth: true
    }
  }
]
