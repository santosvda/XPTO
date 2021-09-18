<template>
  <q-layout view="lHh Lpr lFf">
    
    <q-header class="bg-purple-1 text-grey-10" bordered>
      <q-toolbar class="constrain">
        
        <q-btn flat @click="drawerLeft = !drawerLeft" round dense icon="menu" />
        <q-separator class="large-screen-only" vertical spaced />
        <q-toolbar-title class="text-title text-bold">
          XPTO
        </q-toolbar-title>
        <q-btn to="/" class="large-screen-only" flat round dense icon="eva-log-out" size="18px"/>
      </q-toolbar>
    </q-header>
    <q-drawer
        v-model="drawerLeft"
        :width="300"
        :breakpoint="500"
      >
        <q-scroll-area class="fit bg-blue-grey-1">
          <q-list padding class="menu-list">
            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="eva-home" />
              </q-item-section>

              <q-item-section>
                Home
              </q-item-section>
            </q-item>

            <q-item active clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="eva-shopping-bag" />
              </q-item-section>

              <q-item-section>
                Produtos
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="eva-person" />
              </q-item-section>

              <q-item-section>
                Perfil
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="eva-log-out" />
              </q-item-section>

              <q-item-section>
                Logout
              </q-item-section>
            </q-item>
          </q-list>
        </q-scroll-area>
      </q-drawer>

    <q-footer class="bg-purple-1 text-grey-10 text-body1 text-center" bordered>
      <p>Felipe Dos Santos - 2021 - Github: <a href="https://github.com/santosvda">santosvda</a></p>
    </q-footer>

    <q-page-container class="bg-grey-2">
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>

export default {
  name: 'MainLayout',
  data () {
    return {
      drawerLeft: false,
    }
  }
}
</script>

<style lang="sass">
.q-toolbar
  @media (min-width: $breakpoint-sm-min)
    height: 77px

.q-toolbar__title
  font-size: 30px
  @media (max-width: $breakpoint-xs-max)
    text-align: center

.q-footer
  .q-tab__icon
    font-size:30px
</style>
