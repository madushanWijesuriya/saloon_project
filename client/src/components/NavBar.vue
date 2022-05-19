<template>
  <nav>
    <ul class="menu">
      <li class="logo">
        <router-link exact :style="{ color: '#fff !important' }" to="/"
          >Nayanathara Salons</router-link
        >
      </li>
      <li class="item"><router-link class="item" to="/">Home</router-link></li>
      <li class="item">
        <router-link class="item" to="/products">Products</router-link>
      </li>
      <li class="item">
        <router-link class="item" to="/appointments">Appointments</router-link>
      </li>
      <li class="item"><router-link to="/qrHome">QR</router-link></li>
      <li class="item">
        <router-link class="item" to="/about">About</router-link>
      </li>
      <li class="item" v-if="isUserAdmin">
        <router-link class="item" to="/manageOrders">Manage Orders</router-link>
      </li>
      <li class="item">
        <router-link class="item" to="/myOrders">My Orders</router-link>
      </li>
      <li v-if="isUserAdmin" class="item">
        <router-link to="/reports">Reports</router-link>
      </li>
      <li v-if="isUserLoggedIn" class="item button" @click="logout">
        <a href="#">Log Out</a>
      </li>
      <li class="cart-icon">
        <router-link to="/cart">
          <v-badge
            :content="cartCount"
            :value="cartCount"
            color="green"
            overlap
          >
            <v-icon large color="#f0f0f0"> mdi-cart </v-icon>
          </v-badge>
        </router-link>
      </li>
      <li class="toggle">
        <a href="#"><i class="fas fa-bars"></i></a>
      </li>
    </ul>
  </nav>
</template>
<script>
export default {
  data() {
    return {}
  },
  props: {
    name: {
      type: String,
      required: false,
      default: 'no name'
    }
  },
  computed: {
    isUserLoggedIn() {
      return (
        this.$store.state.user.user !== undefined &&
        this.$store.state.user.user !== null
      )
    },
    user() {
      return this.$store.state.user.user
    },
    isUserAdmin() {
      return this.user && this.user.userRoles
        ? this.user.userRoles.some(role => role === 'Admin')
        : false
    },
    cartCount() {
      return this.$store.getters.getCartCount
    }
  },
  methods: {
    logout() {
      this.$store.dispatch('logoutUser')
      setTimeout(() => {
        this.$router.push('/')
      }, 500)
    },
    toggleMenu() {
      const toggle = document.querySelector('.toggle')
      const menu = document.querySelector('.menu')
      if (menu.classList.contains('active')) {
        menu.classList.remove('active')

        // adds the menu (hamburger) icon
        toggle.querySelector('a').innerHTML = '<i class=’fas fa-bars’></i>'
      } else {
        menu.classList.add('active')

        // adds the close (x) icon
        toggle.querySelector('a').innerHTML = '<i class=’fas fa-times’></i>'
      }
    },
    toggleItem() {
      const menu = document.querySelector('.menu')
      if (menu.classList.contains('submenu-active')) {
        menu.classList.remove('submenu-active')
      } else if (menu.querySelector('.submenu-active')) {
        menu.querySelector('.submenu-active').classList.remove('submenu-active')
        menu.classList.add('submenu-active')
      } else {
        menu.classList.add('submenu-active')
      }
    },
    closeSubmenu(e) {
      const menu = document.querySelector('.menu')
      let isClickInside = menu.contains(e.target)
      if (!isClickInside && menu.querySelector('.submenu-active')) {
        menu.querySelector('.submenu-active').classList.remove('submenu-active')
      }
    }
  },
  mounted() {
    const toggle = document.querySelector('.toggle')
    const items = document.querySelectorAll('.item')
    toggle.addEventListener('click', this.toggleMenu, false)
    for (let item of items) {
      if (item.querySelector('.submenu')) {
        item.addEventListener('click', this.toggleItem, false)
        item.addEventListener('keypress', this.toggleItem, false)
      }
    }
    document.addEventListener('click', this.closeSubmenu, false)
  }
}
</script>
<style lang="scss" scoped>
/* Basic styling */
@import '@/assets/styles/variables.scss';
.router-link-exact-active {
  color: $color1 !important;
}
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}
body {
  font-family: sans-serif;
  font-size: 16px;
}
nav {
  background: $color3;
  padding: 0 15px;
}
a {
  color: white;
  text-decoration: none;
}
.menu,
.submenu {
  list-style-type: none;
  color: white;
}
.logo {
  font-size: 20px;
  color: #fff;
  padding: 7.5px 10px 7.5px 0;
}
.item {
  padding: 10px;
}
.item.button {
  padding: 9px 5px;
}
.item:not(.button) a:hover,
.item a {
  color: $color1;
}
.v-application a {
  color: #fff;
}
.menu::ng-deep li ::ng-deep a:hover {
  color: $color1;
}
/* Mobile menu */
.menu {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: center;
}
.menu li a {
  display: block;
  padding: 15px 5px;
}
.menu li.subitem a {
  padding: 15px;
}
.toggle {
  order: 1;
  font-size: 20px;
}
.item.button {
  order: 2;
}
.item {
  order: 3;
  width: 100%;
  text-align: center;
  display: none;
}
.active .item {
  display: block;
}
.button.secondary {
  /* divider between buttons and menu links */
  border-bottom: 1px #444 solid;
}

/* Submenu up from mobile screens */
.submenu {
  display: none;
}
.submenu-active .submenu {
  display: block;
}
.has-submenu i {
  font-size: 12px;
}
.has-submenu > a::after {
  font-family: 'Font Awesome 5 Free';
  font-size: 12px;
  line-height: 16px;
  font-weight: 900;
  content: '\f078';
  color: white;
  padding-left: 5px;
}
.subitem a {
  padding: 10px 15px;
}
.submenu-active {
  background-color: #111;
  border-radius: 3px;
}

/* Tablet menu */
@media all and (min-width: 700px) {
  .menu {
    justify-content: center;
  }
  .logo {
    flex: 1;
  }
  .item.button {
    width: auto;
    order: 1;
    display: block;
  }
  .toggle {
    flex: 1;
    text-align: right;
    order: 2;
  }
  /* Button up from tablet screen */
  .menu li.button a {
    padding: 10px 15px;
    margin: 5px 0;
  }
  .button a {
    background: #0080ff;
    border: 1px royalblue solid;
  }
  .button.secondary {
    border: 0;
  }
  .button.secondary a {
    // background: transparent;
    border: 1px #0080ff solid;
  }
  .button a:hover {
    text-decoration: none;
  }
  .button:not(.secondary) a:hover {
    background: royalblue;
    border-color: darkblue;
  }
}

/* Desktop menu */
@media all and (min-width: 960px) {
  .menu {
    align-items: flex-start;
    flex-wrap: nowrap;
    background: none;
  }
  .logo {
    order: 0;
  }
  .item {
    order: 1;
    position: relative;
    display: block;
    width: auto;
  }
  .button {
    order: 2;
  }
  .submenu-active .submenu {
    display: block;
    position: absolute;
    left: 0;
    top: 68px;
    background: $color2;
  }
  .toggle {
    display: none;
  }
  .submenu-active {
    border-radius: 0;
  }
  .cart-icon {
    margin: 5px 5px 0 5px;
    order: 999;
  }
}
</style>
