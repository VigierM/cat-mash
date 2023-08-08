<template>
    <v-dialog
        :value="dialog"
        persistent
    >
        <v-card>
            <v-card-title>
                Are you sure you want to delete this cat ?
            </v-card-title>
          <v-card-actions>
            <v-spacer />
            <v-btn
              color="red darken-1"
              text
              @click="$emit('closeDialog', 'deleteCatDialog')"
            >
              No
            </v-btn>
            <v-btn
              color="blue darken-1"
              text
              @click="deleteCat()"
            >
              Yes
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
        async deleteCat () {
            const cat = await this.$api.$delete(`${this.item.id}`)

            if (cat) {
                this.$emit('closeDialog', 'deleteCatDialog')
            } else {
                console.log('Error while deleting cat')
            }
        }
    }
}
</script>
