const state = {
  user: undefined
}

const mutations = {
  logout(state) {
    state.user = undefined
  },
  login(state, user) {
    state.user = { ...user }
  },
  updateUser(state, payload) {
    state.user = { ...state.user, ...payload }
  }
}

const actions = {
  loginUser({ commit }, userData) {
    commit('login', userData)
  },
  logoutUser({ commit }) {
    commit('logout')
  }
}

const getters = {
  getUser: state => state.user
}

export default {
  state,
  actions,
  mutations,
  getters
}
