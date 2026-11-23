import fs from "node:fs";

const path = new URL("../data/capabilities.json", import.meta.url);
const catalog = JSON.parse(fs.readFileSync(path, "utf8"));
const allowedStatuses = new Set(["source-tested", "pages-build", "documented", "proposed"]);
const required = ["id", "area", "title", "meaning", "owner", "status", "statusLabel", "evidence", "example", "sourceUrl", "testUrl", "boundary"];
const errors = [];
const ids = new Set();

if (catalog.schemaVersion !== 1) errors.push("schemaVersion must be 1.");
if (!Array.isArray(catalog.capabilities) || catalog.capabilities.length === 0) errors.push("capabilities must be a non-empty array.");

for (const [index, capability] of (catalog.capabilities ?? []).entries()) {
  for (const field of required) {
    if (typeof capability[field] !== "string" || capability[field].trim().length === 0) {
      errors.push(`capabilities[${index}].${field} must be a non-empty string.`);
    }
  }
  if (capability.id && ids.has(capability.id)) errors.push(`Duplicate capability id: ${capability.id}`);
  if (capability.id) ids.add(capability.id);
  if (capability.status && !allowedStatuses.has(capability.status)) errors.push(`Unknown status for ${capability.id}: ${capability.status}`);
  for (const field of ["ownerUrl", "exampleUrl", "sourceUrl", "testUrl", "guideUrl"]) {
    const value = capability[field];
    if (value && (!value.startsWith("https://github.com/") && !value.startsWith("https://trentbest.github.io/"))) {
      errors.push(`Unsafe or unsupported URL in ${capability.id}.${field}`);
    }
  }
}

if (errors.length) {
  console.error(errors.map(error => "- " + error).join("\n"));
  process.exit(1);
}
console.log(`Validated ${catalog.capabilities.length} capability records.`);
