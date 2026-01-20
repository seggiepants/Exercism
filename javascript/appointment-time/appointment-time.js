// @ts-check

import { time } from "console"

/**
 * Create an appointment
 *
 * @param {number} days
 * @param {number} [now] (ms since the epoch, or undefined)
 *
 * @returns {Date} the appointment
 */
export function createAppointment(days, now = undefined) {
  let current = new Date()
  if (now !== undefined)    
    current = new Date(now)
  current.setDate(current.getDate() + days)
  return current
}

/**
 * Generate the appointment timestamp
 *
 * @param {Date} appointmentDate
 *
 * @returns {string} timestamp
 */
export function getAppointmentTimestamp(appointmentDate) {
  return appointmentDate.toISOString()
}

/**
 * Get details of an appointment
 *
 * @param {string} timestamp (ISO 8601)
 *
 * @returns {Record<'year' | 'month' | 'date' | 'hour' | 'minute', number>} the appointment details
 */
export function getAppointmentDetails(timestamp) {
  let current = new Date(timestamp)
  return {
      year: current.getFullYear(),
      month: current.getMonth(),
      date: current.getDate(),
      hour: current.getHours(),
      minute: current.getMinutes()
  }
}

/**
 * Update an appointment with given options
 *
 * @param {string} timestamp (ISO 8601)
 * @param {Partial<Record<'year' | 'month' | 'date' | 'hour' | 'minute', number>>} options
 *
 * @returns {Record<'year' | 'month' | 'date' | 'hour' | 'minute', number>} the appointment details
 */
export function updateAppointment(timestamp, options) {
  let current = new Date(timestamp)
  if ('year' in options && options.year !== undefined)
    current.setFullYear(options.year)
  if ('month' in options && options.month !== undefined)
    current.setMonth(options.month)
  if ('date' in options && options.date !== undefined)
    current.setDate(options.date)
  if ('hour' in options && options.hour !== undefined)
    current.setHours(options.hour)
  if ('minute' in options && options.minute !== undefined)
    current.setMinutes(options.minute)
  return {
    year: current.getFullYear(),
    month: current.getMonth(),
    date: current.getDate(),
    hour: current.getHours(),
    minute: current.getMinutes()
  }
}

/**
 * Get available time in seconds (rounded) between two appointments
 *
 * @param {string} timestampA (ISO 8601)
 * @param {string} timestampB (ISO 8601)
 *
 * @returns {number} amount of seconds (rounded)
 */
export function timeBetween(timestampA, timestampB) {
  let a = new Date(timestampA)
  let b = new Date(timestampB)
  
  return Math.round((b.getTime() - a.getTime())/1000)
}

/**
 * Get available times between two appointment
 *
 * @param {string} appointmentTimestamp (ISO 8601)
 * @param {string} currentTimestamp (ISO 8601)
 */
export function isValid(appointmentTimestamp, currentTimestamp) {
  let current = new Date(currentTimestamp)
  let appointment = new Date(appointmentTimestamp)
  return appointment > current
}
