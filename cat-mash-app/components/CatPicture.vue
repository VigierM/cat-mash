<template>
    <div v-if="source != null">
        <v-img
            :src="source"
            contain
            :max-height="size"
            :max-width="size"
            class="img-circle"
            @click="$emit('click')"
        />
    </div>
</template>

<script>
export default {
    props: {
        source: {
            type: String,
            default: null
        },
        size: {
            type: Number,
            default: 350
        }
    }
}
</script>

<style scoped>
.img-circle {
    border-radius: 50%;
}
</style>
