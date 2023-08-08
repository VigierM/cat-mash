<template>
    <v-dialog
        :value="dialog"
        persistent
    >
        <v-card>
            <v-card-title>
                Create new Cat
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
              @click="$emit('closeDialog', 'createCatDialog')"
            >
              Close
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="createNewCat()"
            >
              Create
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
        async createNewCat () {
            const cat = await this.$api.$post(null, {
                imageUrl: this.form.imageUrl
            })

            if (cat != null) {
                this.$emit('closeDialog', 'createCatDialog')
            } else {
                console.log('Error while creating new cat')
            }
        }
    }
}
</script>
