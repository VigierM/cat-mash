<template>
    <v-container>
        <v-row>
            <v-col>
                <h1>
                    Miaouh administration
                </h1>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12">
                    <v-data-table
                        :headers="tableHeaders"
                        :items="cats.data"
                        item-key="id"
                        :options.sync="tableOptions"
                        :items-per-page="20"
                        class="elevation-1"
                        :server-items-length="cats.totalCount"
                        :loading="cats.data?.length === 0"
                        :footer-props="{
                            'items-per-page-options': [10, 20]
                        }"
                        loading-text="Loading... Please wait"
                    >
                        <template v-slot:top>
                            <v-toolbar
                                flat
                            >
                                <v-toolbar-title>
                                    Miaouh CRUD
                                </v-toolbar-title>
                                <v-divider
                                    class="mx-4"
                                    inset
                                    vertical
                                />
                                <v-spacer />
                                <v-btn
                                    @click="dialogs.createCatDialog.open = true"
                                >
                                    +
                                </v-btn>
                            </v-toolbar>
                        </template>

                        <template
                            #[`item.update`]="{ item }"
                            >
                            <v-icon
                                @click="openUpdateCatDialog(item)"
                            >
                                mdi-update
                            </v-icon>
                        </template>

                        <template
                            #[`item.delete`]="{ item }"
                            >
                            <v-icon
                                @click="openDeleteCatDialog(item)"
                            >
                                mdi-delete
                            </v-icon>
                        </template>
                    </v-data-table>
            </v-col>
        </v-row>
        <v-row>
            <CreateCatDialog
                :dialog="dialogs.createCatDialog.open"
                @closeDialog="closeDialog($event)"
            />

            <UpdateCatDialog
                :dialog="dialogs.updateCatDialog.open"
                :item="dialogs.updateCatDialog.item"
                @closeDialog="closeDialog($event)"
            />

            <DeleteCatDialog
                :dialog="dialogs.deleteCatDialog.open"
                :item="dialogs.deleteCatDialog.item"
                @closeDialog="closeDialog($event)"
            />
        </v-row>
    </v-container>
</template>

<script>
import CreateCatDialog from '../../components/Admin/Dialogs/CreateCatDialog.vue'
import DeleteCatDialog from '../../components/Admin/Dialogs/DeleteCatDialog.vue'
import UpdateCatDialog from '../../components/Admin/Dialogs/UpdateCatDialog.vue'
export default {

    components: {
        CreateCatDialog,
        UpdateCatDialog,
        DeleteCatDialog
    },

    data () {
        return {
            tableHeaders: [
                { text: 'Id', align: 'start', sortable: true, value: 'id' },
                { text: 'Image URL', sortable: false, value: 'imageUrl' },
                { text: 'Nombre de votes', sortable: true, value: 'votes' },
                { text: 'Mettre à jour', value: 'update' },
                { text: 'Supprimer', value: 'delete' }
            ],
            tableOptions: {
                page: 1,
                itemsPerPage: 20,
                sortBy: ['Votes'],
                sortDesc: [true]
            },
            cats: [],
            dialogs: {
                createCatDialog: {
                    open: false
                },
                updateCatDialog: {
                    open: false,
                    item: null
                },
                deleteCatDialog: {
                    open: false,
                    item: null
                }
            }
        }
    },

    watch: {
        tableOptions: {
            deep: true,
            handler: 'getCatsList'
        }
    },

    async created () {
        await this.getCatsList()
    },

    methods: {
        async getCatsList () {
            const sortColumnName = this.tableOptions.sortBy[0]
            const capitalizedColumnName = sortColumnName?.charAt(0).toUpperCase() + sortColumnName?.slice(1)

            this.cats = await this.$api.$get(null, {
                params: {
                    pageNumber: this.tableOptions.page,
                    pageSize: this.tableOptions.itemsPerPage,
                    sortColumnName: capitalizedColumnName,
                    sortOrder: this.tableOptions.sortDesc[0] ? 'DESC' : 'ASC'
                }
            })
        },

        openUpdateCatDialog (cat) {
            console.log(cat)
            this.dialogs.updateCatDialog.item = cat
            this.dialogs.updateCatDialog.open = true
        },

        openDeleteCatDialog (cat) {
            this.dialogs.deleteCatDialog.item = cat
            this.dialogs.deleteCatDialog.open = true
        },

        async closeDialog (source) {
            this.dialogs[source].open = false
            this.dialogs[source].item = null
            await this.getCatsList()
        }
    }
}
</script>
