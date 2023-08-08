<template>
    <v-dialog
        :value="dialog"
        persistent
    >
        <v-card>
            <v-card-title>
                Update Cat
            </v-card-title>

            <v-card-text>
                <v-container>
                    <v-row>
                        <v-col>
                            <v-text-field
                                v-model="form.imageUrl"
                                label="Url de l'image"
                                hint="Url de l'image"
                                required
                            />
                        </v-col>
                    </v-row>
                </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer />
            <v-btn
              color="blue darken-1"
              text
              @click="$emit('closeDialog', 'updateCatDialog')"
            >
              Close
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="updateCat()"
            >
              Update
            </v-btn>
          </v-card-actions>
        </v-card>

    </v-dialog>
</template>

<script>
export default {
    props: {
        dialog: {
            type: Boolean,
            default: false
        },
        item: {
            type: Object,
            default: null
        }
    },

    data () {
        return {
            form: {
                imageUrl: null
            }
        }
    },

    methods: {
        async updateCat () {
            const cat = await this.$api.$put(`${this.item.id}`, {
                imageUrl: this.form.imageUrl
            })

            if (cat != null) {
                this.$emit('closeDialog', 'updateCatDialog')
            } else {
                console.log('Error while updating cat')
            }
        }
    }
}
</script>
