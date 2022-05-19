export const productRoutes = [
  {
    path: '/products',
    name: 'Products',
    component: () =>
      import(/* webpackChunkName: "about" */ '../views/products/Products.vue'),
    meta: {
      isRequiredAuth: false
    }
  }
]
