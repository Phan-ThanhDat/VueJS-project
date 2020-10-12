<template>
    <a-root-page-layout>
        <template #page>
            <a-pane
                columns
                content-grow
            >
                <a-card
                    content-grow
                    scrollable
                    align-items="flex-start"
                >
                    <h1>Resume form</h1>
                    <label for="firstName">First name:</label>
                    <a-input
                        id="firstName"
                        required
                        v-model="form.firstName"
                    />
                </a-card>
                <button @click="fetchResume"> clcik aaa</button>
            </a-pane>
        </template>
    </a-root-page-layout>
</template>

<script lang="ts">
    import BaseView from 'views/BaseView.ts.vue'
    import {mapActions} from "vuex";

    interface Methods {
        // fetchResume: (commit: any) => void
    }

    /**
     * Resume view
     */
    export default BaseView.extend<void, Methods, void, void>({
        data: () => ({
            form: {
                firstName: ""
            },

        }),
        methods: {
            fetchResume () {
                console.log('commit ...fetch')
                this.$store.dispatch('ResumeActions.Fetch')
                console.log('this.$store.state',this.$store)
            }
            // ...mapActions(['ResumeActions!.Fetch'])
        }

    })
</script>
