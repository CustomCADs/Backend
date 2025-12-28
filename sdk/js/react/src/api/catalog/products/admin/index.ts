import { axios } from '@/api/axios';
import * as removeResources from './remove';
import * as addTagResources from './add-tag';
import * as removeTagResources from './remove-tag';

export const remove = async (req: removeResources.Request) =>
	await axios.patch(removeResources.url(), req);

export const addTag = async (req: addTagResources.Request) =>
	await axios.patch(addTagResources.url(), req);

export const removeTag = async (req: removeTagResources.Request) =>
	await axios.patch(removeTagResources.url(), req);
