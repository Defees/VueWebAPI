<!-- eslint-disable vue/multi-word-component-names -->
<template>
    <div>
        About page
        <br>
        <button @click="()=> {router.push('/login')}">To login</button>
        <button @click="()=> {router.push('/register')}">To register</button>
    </div>
</template>
   
<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter();


</script>