<!-- eslint-disable vue/multi-word-component-names -->
<template>
    <div>
        Profile page
        <br>
        <button @click="()=> {router.push('login/')}">this is Profile page, go back</button>
    </div>
</template>
   
<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter();


</script>