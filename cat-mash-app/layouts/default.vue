<template>
 <v-app>
    <v-app-bar
      :clipped-left="clipped"
      fixed
      app
    >
      <v-container fill-height fluid>
        <v-row justify="center" align="center">
          <v-col class="d-flex">
            <NuxtLink
              class="pa-2"
              to="/"
            >
              Mashup
            </NuxtLink>
            <NuxtLink
              class="pa-2"
              to="/ranks"
            >
              Ranks
            </NuxtLink>
          </v-col>
          <v-col>
            <h2 class="text-center">
              Cat Mash
            </h2>
          </v-col>
          <v-col align="end">
            <NuxtLink
              class="pa-2"
              to="/admin"
            >
              Cat administration
            </NuxtLink>
          </v-col>
        </v-row>
      </v-container>
    </v-app-bar>
    <v-main>
      <v-container fill-height>
        <Nuxt />
      </v-container>
    </v-main>
    <v-footer
      :absolute="!fixed"
      app
    >
      <span>&copy; Sweetie Inc Cat Mash {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>

</template>

<script>
export default {
  name: 'DefaultLayout',
  data () {
    return {
      clipped: false,
      drawer: false,
      fixed: false,
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Cat Mash'
    }
  }
}
</script>
