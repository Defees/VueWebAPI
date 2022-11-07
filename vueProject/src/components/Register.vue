<!-- eslint-disable vue/multi-word-component-names -->
<template>
    <div>
        Registration page
    </div>
    <div>
        <Form @submit="handleRegister" :validation-schema="schema">
            <div v-if="!successful">
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
                    <label for="confirmPassword">Confirm password</label>
                    <Field name="confirmPassword" type="password" class="form-control" />
                    <ErrorMessage name="confirmPassword" class="error-feedback" />
                </div>

                <div class="form-group">
                    <button class="btn btn-primary btn-block" :disabled="loading">
                        <span v-show="loading" class="spinner-border spinner-border-sm"></span>
                        <span>Register</span>
                    </button>
                </div>

                <div class="form-group">
                    <div v-if="message" class="alert alert-danger" role="alert">
                        {{ message }}
                    </div>
                </div>
            </div>
        </Form>
    </div>
</template>
   
<script setup lang="ts">
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
import { useRouter } from 'vue-router'
import { useAuthStore } from "@/store/use-auth-store";
import { onMounted } from "vue";

const authStore = useAuthStore();
let successful = false;
let loading = false;
let message = "";
const schema = yup.object().shape({
    email: yup
        .string()
        .required("Email is required!")
        .email("Email is invalid!")
        .max(50, "Must be maximum 50 characters!"),
    password: yup
        .string()
        .required("Password is required!")
        .min(6, "Must be at least 6 characters!")
        .max(40, "Must be maximum 40 characters!"),
    confirmpassword: yup
        .string()
        .required("Confirm Password is required!")
        .oneOf([yup.ref('password'), null], "Passwords don't match!")
});
const router = useRouter();

onMounted(() => {
    if (authStore.state.status.loggedIn)
        router.push("/profile");
});

function handleRegister(user: any) {
    loading = true;

    authStore.register(user).then(
        () => {
            router.push("/profile")
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
    )
}
</script>