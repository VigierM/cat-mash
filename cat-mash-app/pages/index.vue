<template>
    <v-container
        fluid
        fill-height
        class="d-flex flex-row align-start"
    >
        <v-row>
            <v-col cols="12">
                <h1 class="text-center">
                    Clique sur le plus beau !
                </h1>
            </v-col>
        </v-row>
        <v-row>
            <v-col
                v-for="(cat, index) in currentMashup.data"
                :key="index"
                cols="6"
                class="d-flex flex-fill justify-center align-center"
            >
                <v-hover v-slot="{ hover }">
                    <cat-picture
                        :source="cat.imageUrl"
                        :size="hover ? 450 : 350"
                        @click="postCatVote(cat)"
                    />
                </v-hover>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import CatPicture from '../components/CatPicture.vue'

export default {
    components: {
        CatPicture
    },

    data () {
        return {
            currentMashup: []
        }
    },

    async created () {
        await this.getNewMashup()
    },

    methods: {
        async getNewMashup () {
            this.currentMashup = await this.$api.$get('mashup')
        },

        async postCatVote (cat) {
            console.log('Voting')
            console.log(cat)
            const voted = await this.$api.$post(`${cat.id}/votes`)
            if (voted) {
                this.getNewMashup()
            }
        }
    }
}
</script>

<style scoped>
.card-circle {
    border-radius: 50%;
}
</style>
