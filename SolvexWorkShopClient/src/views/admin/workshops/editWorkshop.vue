<template>
  <section>
    <nav class="navbar">
      <div class="navbar-brand">
        <span class="navbar-item title is-2">Editar WorkShop</span>
      </div>
      <div class="navbar-end">
        <div class="navbar-item">
          <div class="field">
            <b-switch type="is-twitter" v-model="exitOnSave"
              >Salir al guardar</b-switch
            >
          </div>
        </div>
      </div>
    </nav>
    <b-loading :is-full-page="false" :active="loading || saving"></b-loading>

    <form @submit.prevent="validate">
      <b-collapse class="card" animation="slide" aria-id="contentIdForA11y3">
        <template #trigger="props">
          <div
            class="card-header"
            role="button"
            aria-controls="contentIdForA11y3"
          >
            <p class="card-header-title">Datos del WorkShop</p>
            <a class="card-header-icon">
              <b-icon :icon="props.open ? 'menu-down' : 'menu-up'"> </b-icon>
            </a>
          </div>
        </template>

        <div class="card-content">
          <div class="content">
            <div class="columns is-multiline">
              <div class="column">
                <b-field
                  label="Nombre*"
                  :type="{ 'is-danger': errors.has('name') }"
                  :message="errors.first('name')"
                >
                  <b-input
                    v-model="model.name"
                    name="name"
                    data-vv-as="nombre"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <b-field
                  label="Descripción*"
                  :type="{ 'is-danger': errors.has('description') }"
                  :message="errors.first('description')"
                >
                  <b-input
                    v-model="model.description"
                    name="description"
                    type="textarea"
                    data-vv-as="descripción"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <b-field
                  label="Fecha de inicio*"
                  :type="{ 'is-danger': errors.has('startDate') }"
                  :message="errors.first('startDate')"
                >
                  <b-datepicker
                    v-model="model.startDate"
                    name="startDate"
                    data-vv-as="fecha de inicio"
                    v-validate="'required'"
                    placeholder="dd/MM/yyyy"
                    icon="calendar-today"
                    locale="es"
                  >
                  </b-datepicker>
                </b-field>
              </div>
              <div class="column">
                <b-field
                  label="Contenido de apoyo*"
                  :type="{ 'is-danger': errors.has('contentSupport') }"
                  :message="errors.first('contentSupport')"
                >
                  <b-input
                    v-model="model.contentSupport"
                    name="contentSupport"
                    type="textarea"
                    data-vv-as="contentSupport"
                    v-validate="'required'"
                    placeholder="Requerido"
                  />
                </b-field>

                <b-field
                  label="Fecha de fin*"
                  :type="{ 'is-danger': errors.has('endDate') }"
                  :message="errors.first('endDate')"
                >
                  <b-datepicker
                    v-model="model.endDate"
                    name="endDate"
                    data-vv-as="Fecha de fin"
                    placeholder="dd/MM/yyyy"
                    icon="calendar-today"
                    locale="es"
                  >
                  </b-datepicker>
                </b-field>
              </div>
            </div>
          </div>
        </div>
      </b-collapse>

      <br />

      <nav class="level">
        <!-- Left side -->
        <div class="level-left">
          <p class="level-item">
            <b-button
              :disabled="(modelDoNotChange && !errors.any()) || saving"
              size="is-medium"
              type="is-light"
              @click="clean"
              icon-right="eraser"
              >Reiniciar</b-button
            >
          </p>

          <p class="level-item">
            <b-button
              size="is-medium"
              type="is-danger"
              :disabled="saving"
              @click="cancel"
              icon-right="cancel"
              >Cancelar</b-button
            >
          </p>
        </div>

        <!-- Right side -->
        <div class="level-right">
          <p class="level-item">
            <b-button
              size="is-medium"
              type="is-primary"
              native-type="submit"
              :disabled="errors.any()"
              :loading="saving"
              icon-right="content-save"
              >Guardar</b-button
            >
          </p>
        </div>

      </nav>
    </form>
  </section>
</template>
<script lang="ts">
import { Component, Mixins, Vue } from "vue-property-decorator";
import { BaseFormEditMixin } from "@/mixins";
import { WorkShop } from "@/core/model";

@Component({
  components: {},
})
export default class WorkShopEditComponent extends Mixins<
  BaseFormEditMixin<WorkShop>
>(BaseFormEditMixin) {
  constructor() {
    super();
    this.controller = "WorkShop";
    this.model = new WorkShop();
  }

  async getData() {
    try {
      if (this.modelToEdit && !this.loadFromAPI)
        this.model = this.map<WorkShop>(this.modelToEdit);
      else {
        this.loading = true;
        const response = await this.service.getById(this.id);
        this.model = response.data;
        this.model.startDate = new Date(this.model.startDate as string);
        this.model.endDate = new Date(this.model.endDate as string);
      }
      this.modelInit = this.map<WorkShop>(this.model);
    } catch (errors) {
      this.operationFailedErrors(errors);
    } finally {
      this.loading = false;
    }
  }
}
</script>

<style lang="css">
.p-2rem {
  padding: 2rem;
}
</style>