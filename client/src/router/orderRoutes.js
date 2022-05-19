export const orderRoutes = [
  {
    path: '/cart',
    name: 'Cart',
    component: () =>
      import(/*webpackChunkName: "Cart" */ '@/views/Order/CartView.vue'),
    meta: {
      isRequiredAuth: true
    }
  },
  {
    path: '/placeOrder',
    name: 'PlaceOrder',
    component: () =>
      import(
        /*webpackChunkName: "PlaceOrder" */ '@/views/Order/PlaceOrder.vue'
      ),
    meta: {
      isRequiredAuth: true
    }
  },
  {
    path: '/manageOrders',
    name: 'ManageOrders',
    component: () =>
      import(
        /*webpackChunkName: "ManageOrders" */ '@/views/Order/ManageOrders.vue'
      ),
    meta: {
      isRequiredAuth: true
    }
  },
  {
    path: '/myOrders',
    name: 'Orders',
    component: () =>
      import(/*webpackChunkName: "MyOrders" */ '@/views/Order/MyOrders.vue'),
    meta: {
      isRequiredAuth: true
    }
  }
]
