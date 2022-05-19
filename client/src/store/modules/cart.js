const state = {
  cart: []
}

const mutations = {
  addToCart(state, payload) {
    state.cart = [...state.cart, payload]
  },
  removeFromCart(state, payload) {
    state.cart = state.cart.filter(c => c !== payload)
  },
  clearAllFromCart(state) {
    state.cart = []
  }
}

const actions = {
  addToCart({ commit }, product) {
    if (product.quentity > 0) {
      commit('addToCart', product)
    }
  },
  removeFromCart({ commit }, product) {
    if (product) {
      commit('removeFromCart', product)
    }
  },
  clearAllFromCart({ commit }) {
    commit('clearAllFromCart')
  }
}

const getters = {
  getCartCount(state) {
    return state.cart.length
  },
  getCartData(state) {
    return state.cart
  }
}

export default {
  state,
  actions,
  mutations,
  getters
}
