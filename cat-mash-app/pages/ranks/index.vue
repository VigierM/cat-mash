<template>
    <v-container
        fluid
        fill-height
        class="d-flex flex-row align-start"
    >
        <v-row>
            <v-col class="text-center">
                <h1>
                    Classement des chats
                </h1>
            </v-col>
        </v-row>
        <v-row>
            <v-col class="text-center">
                <v-container
                    fluid
                    grid-list
                >
                    <v-layout row wrap>
                        <v-card
                            v-for="(cat, index) in rankList.data"
                            :key="index"
                            height="400px"
                            width="400px"
                            align="center"
                        >
                            <v-card-title>
                                {{ '#' + (((currentPage - 1) * 20) + (index + 1))}}
                            </v-card-title>
                            <v-card-text
                                class="d-flex flex-column"
                            >
                                <div>
                                    <CatPicture :source="cat.imageUrl" :size="275" />
                                </div>
                                <div class="justify-end">
                                    <h3>
                                        Nombre de votes: {{ cat.votes }}
                                    </h3>
                                </div>
                            </v-card-text>
                        </v-card>
                    </v-layout>
                </v-container>
            </v-col>
        </v-row>
        <v-row justify="center">
            {{ numberOfPages }}
            <v-pagination
                v-model="currentPage"
                :length="numberOfPages"
                :total-visible="5"
            ></v-pagination>
        </v-row>
    </v-container>
</template>

<script>
import CatPicture from '../../components/CatPicture.vue'

export default {

    components: {
        CatPicture
    },

    data () {
        return {
            rankList: [],
            currentPage: 1,
            numberOfPages: 5
        }
    },

    watch: {
        async currentPage () {
            console.log(this.currentPage)
            this.rankList = []
            await this.getRankList()
        }
    },

    async created () {
        await this.getRankList()
    },

    methods: {
        async getRankList () {
            this.rankList = await this.$api.$get(null, {
                params: {
                    pageNumber: this.currentPage,
                    sortColumnName: 'Votes',
                    sortOrder: 'DESC'
                }
            })
            this.numberOfPages = Math.ceil(this.rankList?.totalCount / 20)
        }
    }
}
</script>
