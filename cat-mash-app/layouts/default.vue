<template>
 <v-app>
    <v-app-bar
      :clipped-left="clipped"
      fixed
      app
    >
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col>
            <h2 class="text-center">
              Cat Mash
            </h2>
          </v-col>
        </v-row>
      </v-container>
    </v-app-bar>
    <v-main>
      <div>
        <Nuxt />
      </div>
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
