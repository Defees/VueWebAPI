<template>
  <div id="main" class="d-flex align-items-center justify-content-center">
    <div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
const router = useRouter();

onMounted(() => {
    router.push("/home");
});

</script>

<style>
#main {
  min-height: 100vh;
}
</style>
