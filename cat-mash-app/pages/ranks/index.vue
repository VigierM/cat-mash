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
                                {{ '#' + (((page - 1) * 20) + (index + 1))}}
                            </v-card-title>
                            <div>
                                <CatPicture :source="cat.imageUrl" :size="300" />
                            </div>
                            <div>
                                <h3>
                                    Nombre de votes: {{ cat.votes }}
                                </h3>
                            </div>
                        </v-card>
                    </v-layout>
                </v-container>
            </v-col>
        </v-row>
        <v-row justify="center">
            <v-pagination
                v-model="page"
                :length="rankList.totalCount / 20"
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
            page: 1
        }
    },

    watch: {
        async page () {
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
                    pageNumber: this.page,
                    sortColumnName: 'Votes',
                    sortOrder: 'DESC'
                }
            })
        }
    }
}
</script>
