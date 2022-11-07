<!-- eslint-disable vue/multi-word-component-names -->
<template>
   <div class="card card-container">
      <Form @submit="handleLogin" :validation-schema="schema">
        <div class="form-group">
          <label for="email">Email</label>
          <Field name="email" type="text" class="form-control" />
          <ErrorMessage name="email" class="error-feedback" />
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <Field name="password" type="password" class="form-control" />
          <ErrorMessage name="password" class="error-feedback" />
        </div>

        <div class="form-group">
          <button class="btn btn-primary btn-block" :disabled="loading">
            <span
              v-show="loading"
              class="spinner-border spinner-border-sm"
            ></span>
            <span>Login</span>
          </button>
        </div>

        <div class="form-group">
          <div v-if="message" class="alert alert-danger" role="alert">
            {{ message }}
          </div>
        </div>
      </Form>
   </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useAuthStore } from "@/store/use-auth-store";
import { Form, Field, ErrorMessage } from "vee-validate";
import { useRouter } from 'vue-router'
import * as yup from "yup";

const authStore = useAuthStore();
const router = useRouter();
let loading = false;
let message = "";

const schema = yup.object().shape({
      email: yup.string().required("Username is required!"),
      password: yup.string().required("Password is required!"),
    });

onMounted(() => {
   if (authStore.state.status.loggedIn) {
      router.push("/profile");
    }
})

async function handleLogin(user: any) {
      loading = true;

      await authStore.login(user).then(
        () => {
          router.push("/profile");
        },
        (error) => {
          loading = false;
          message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
        }
      );
    }
</script>

